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

        private List<Alarm> alarms;
        private List<Tag> tags;
        private List<AlarmRecord> alarmRecords;
        private List<TagRecord> tagRecords;

        public ScanService(TagRepository tagRepository, AlarmRepository alarmRepository, DeviceRepository deviceRepository, IHubContext<TagHub> tagHub, IHubContext<AlarmHub> alarmHub, TagHandler tagHandler)
        {
            this.tagRepository = tagRepository;
            this.alarmRepository = alarmRepository;
            this.deviceRepository = deviceRepository;
            this.tagHub = tagHub;
            this.alarmHub = alarmHub;
            this.tagHandler = tagHandler;
            this.alarms = new List<Alarm>();
            this.alarmRecords = new List<AlarmRecord>();
            this.tags = new List<Tag>();
            this.tagRecords = new List<TagRecord>();
        }

        public void Run()
        {
            foreach (Alarm alarm in alarmRepository.GetAll())
            {
                this.alarms.Add(alarm);
            }
            foreach (Tag tag in tagRepository.GetAllTags())
            {
                if (tag.TagType.Equals(TagType.AO) || tag.TagType.Equals(TagType.DO)) continue;
                this.tags.Add(tag);
            }
            foreach (Tag tag in tagRepository.GetAllTags())
            {
                if (tag.TagType.Equals(TagType.AO) || tag.TagType.Equals(TagType.DO)) continue;
                Thread thread = new Thread(() => StartSimulationThread(tag));
                thread.Start();
            }
        }

        public void UpdateDatabase()
        {
            while (true)
            {
                lock (Utils._lock)
                {
                    tagRepository.UpdateAllTags(this.tags);
                    tagRepository.InsertAllTagRecords(this.tagRecords);
                    tagRecords.Clear();
                    alarmRepository.InsertAllAlarmRecords(this.alarmRecords);
                    alarmRecords.Clear();
                }
                Thread.Sleep(3000);
            }

        }
        public void AddNewTag(Tag tag)
        {
            this.tags.Add(tag);
            Thread thread = new Thread(() => StartSimulationThread(tag));
            thread.Start();
        }

        public void ToggleScan(int id)
        {
            this.tags.Find(t => t.Id == id).IsScanOn = !this.tags.Find(t => t.Id == id).IsScanOn;
        }

        public void DeleteTag(int id)
        {
            this.tags.Remove(this.tags.Find(t => t.Id == id));
        }

        public void EditTag(Tag tag)
        {
            //tags.Find(t => t.Id == tag.Id) = tag;
        }

        private void StartSimulationThread(object obj)
        {
            Tag tag = (Tag)obj;
            double currentValue = -1000;
            Alarm currentAlarm;
            List<Alarm> alarms = new();

            while (true)
            {
                tag = this.tags.Find(t => t.Id == tag.Id);
                alarms = this.alarms.FindAll(t => t.TagId == tag.Id);
                
                if (tag == null)
                {
                    break;
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
                            currentValue = DeviceRepository.devices[tag.IOAddress];
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
                        tags.Find(t => t.Id == tag.Id).Value = currentValue;
                    }

                    if (currentAlarm != null)
                    {
                        if (currentAlarm.Type == Type.HIGHER) { currentValue = (double)tag.HighLimit; }
                        else { currentValue = (double)tag.LowLimit; }

                        AlarmRecord alarmRecord = new AlarmRecord { AlarmId = currentAlarm.Id, Timestamp = DateTime.Now, TagId = tag.Id };

                        lock (Utils._lock)
                        {
                            alarmHub.Clients.All.SendAsync("alarm", new AlarmRecordDTO { TagId = tag.Id, Priority = currentAlarm.Priority, Type = currentAlarm.Type, Value = currentAlarm.Value });
                            alarmRecords.Add(alarmRecord);
                        }
                    }

                    TagRecord tagRecord = new TagRecord { Value = currentValue, Timestamp = DateTime.Now, TagId = tag.Id, HighLimit = tag.HighLimit, LowLimit = tag.LowLimit };

                    lock (Utils._lock)
                    {
                        tagRecords.Add(tagRecord);
                        tagHub.Clients.All.SendAsync("tag", tagRecord);
                        tagHandler.SendDataToClient("tag", tagRecord);
                    }
                }

                Thread.Sleep((int)tag.ScanTime);
            }
        }
    }
}
