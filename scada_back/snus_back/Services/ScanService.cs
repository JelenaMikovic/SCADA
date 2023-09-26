using Microsoft.AspNetCore.SignalR;
using scada_back.DTOs;
using scada_back.Handlers;
using scada_back.HandlersHandlers;
using scada_back.Models;
using scada_back.Repositories;
using Type = scada_back.Models.Type;
using System;
using System.Collections.Generic;
using System.Threading;

namespace scada_back.Services
{
    public class ScanService
    {
        private readonly TagRepository tagRepository;
        private readonly AlarmRepository alarmRepository;
        private readonly DeviceRepository deviceRepository;
        private readonly IHubContext<AlarmHub> alarmHub;
        private readonly IHubContext<TagHub> tagHub;
        private readonly TagHandler tagHandler;

        public ScanService(TagRepository tagRepository, AlarmRepository alarmRepository, DeviceRepository deviceRepository, IHubContext<TagHub> tagHub, IHubContext<AlarmHub> alarmHub, TagHandler tagHandler)
        {
            this.tagRepository = tagRepository;
            this.alarmRepository = alarmRepository;
            this.deviceRepository = deviceRepository;
            this.tagHub = tagHub;
            this.alarmHub = alarmHub;
            this.tagHandler = tagHandler;
        }

        public void Run()
        {
            foreach (Tag tag in tagRepository.GetAllTags())
            {
                if (tag.TagType.Equals(TagType.AO) || tag.TagType.Equals(TagType.DO)) continue;
                Thread thread = new Thread(() => StartSimulationThread(tag));
                thread.Start();
            }
        }

        public void AddNewTag(Tag tag)
        {
            Thread thread = new Thread(() => StartSimulationThread(tag));
            thread.Start();
        }

        private void StartSimulationThread(object obj)
        {
            Tag tag = (Tag)obj;
            double currentValue = -1000;
            Alarm currentAlarm;
            List<Alarm> alarms;

            while (true)
            {
                lock (Utils._lock)
                {
                    tag = tagRepository.GetTagById(tag.Id);
                    if (tag == null)
                    {
                        break;
                    }
                    alarms = alarmRepository.GetByTagId(tag.Id);
                }

                if ((bool)tag.IsScanOn)
                {
                    if (tag.IOAddress == "SIN" || tag.IOAddress == "COS" || tag.IOAddress == "RAMP")
                    {
                        currentValue = SimulationService.CalculateValue(tag.IOAddress);
                    }
                    else
                    {
                        lock (Utils._lock)
                        {
                            // Lock the access to deviceRepository when reading the value
                            currentValue = deviceRepository.GetByIOAddress(tag.IOAddress).Value;
                        }
                    }
                    currentAlarm = null;

                    foreach (Alarm alarm in alarms)
                    {
                        if ((alarm.Type == Type.HIGHER && currentValue >= alarm.Value) || (alarm.Type == Type.LOWER && currentValue <= alarm.Value))
                        {
                            if (currentAlarm == null || (currentAlarm != null && currentAlarm.Priority < alarm.Priority))
                            {
                                currentAlarm = alarm;
                            }
                        }
                    }

                    lock (Utils._lock)
                    {
                        tag.Value = currentValue;
                        tagRepository.UpdateTag(tag);
                    }

                    if (currentAlarm != null)
                    {
                        if (currentAlarm.Type == Type.HIGHER) { currentValue = (double)tag.HighLimit; }
                        else { currentValue = (double)tag.LowLimit; }

                        AlarmRecord alarmRecord = new AlarmRecord { AlarmId = currentAlarm.Id, Timestamp = DateTime.Now, TagId = tag.Id };

                        lock (Utils._lock)
                        {
                            alarmHub.Clients.All.SendAsync("alarm", new AlarmRecordDTO { TagId = tag.Id, Priority = currentAlarm.Priority, Type = currentAlarm.Type, Value = currentAlarm.Value });
                            alarmRepository.AddRecord(alarmRecord);
                        }
                    }

                    TagRecord tagRecord = new TagRecord { Value = currentValue, Timestamp = DateTime.Now, TagId = tag.Id, HighLimit = tag.HighLimit, LowLimit = tag.LowLimit };

                    lock (Utils._lock)
                    {
                        //tagRepository.AddRecord(tagRecord);
                        tagHub.Clients.All.SendAsync("tag", tagRecord);
                        tagHandler.SendDataToClient("tag", tagRecord);
                    }
                }

                Thread.Sleep((int)tag.ScanTime);
            }
        }
    }
}
