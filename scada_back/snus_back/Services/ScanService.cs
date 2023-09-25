using Microsoft.AspNetCore.SignalR;
using scada_back.DTOs;
using scada_back.Handlers;
using scada_back.HandlersHandlers;
using scada_back.Models;
using scada_back.Repositories;
using Type = scada_back.Models.Type;

namespace scada_back.Services
{
    public class ScanService
    {
        private TagRepository tagRepository;
        private AlarmRepository alarmRepository;
        private DeviceRepository deviceRepository;
        private readonly IHubContext<AlarmHub> alarmHub;
        private readonly IHubContext<TagHub> tagHub;
        private TagHandler tagHandler;
        private readonly object _lock = new();

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
            foreach(Tag tag in tagRepository.GetAllTags()) {
                if (tag.TagType.Equals(TagType.AO) || tag.TagType.Equals(TagType.DO)) continue;
                Thread thread;
                thread = new Thread(StartSimulationThread);
                thread.Start(tag);
            }
        }

        public void AddNewTag(Tag tag)
        {
            Thread thread;
            thread = new Thread(StartSimulationThread);
            thread.Start(tag);
        }

        private void StartSimulationThread(object? obj)
        {
            Tag tag = (Tag)obj;
            double currentValue = -1000;
            Alarm currentAlarm;

            while (true)
            {
                tag = tagRepository.GetTagById(tag.Id);
                List<Alarm> alarms = alarmRepository.GetByTagId(tag.Id);
                if (tag == null)
                {
                    break;
                }
                if ((bool)tag.IsScanOn)
                {
                    if (tag.IOAddress == "SIN" || tag.IOAddress == "COS" || tag.IOAddress == "RAMP") { 
                        currentValue = SimulationService.CalculateValue(tag.IOAddress);
                    } else
                    {
                        currentValue = deviceRepository.GetByIOAddress(tag.IOAddress).Value;
                    }
                    currentAlarm = null;
                    foreach (Alarm alarm in alarms)
                    {
                        if ((alarm.Type == Type.HIGHER && currentValue >= alarm.Value) || (alarm.Type == Type.LOWER && currentValue <= alarm.Value))
                        {
                            if ((currentAlarm == null) || (currentAlarm != null && currentAlarm.Priority < alarm.Priority))
                            {
                                currentAlarm = alarm;
                            }
                        }
                    }
                    lock (_lock)
                    {
                        tag.Value = currentValue;
                        tagRepository.UpdateTag(tag);
                    }

                    if (currentAlarm != null)
                    {
                        if (currentAlarm.Type == Type.HIGHER){ currentValue = (double)tag.HighLimit; }
                        else { currentValue = (double)tag.LowLimit; }

                        AlarmRecord alarmRecord = new AlarmRecord { AlarmId = currentAlarm.Id, Timestamp = DateTime.Now, TagId = tag.Id };

                        lock (_lock)
                        {
                            alarmHub.Clients.All.SendAsync("alarm", new AlarmRecordDTO { TagId = tag.Id, Priority = currentAlarm.Priority, Type = currentAlarm.Type, Value = currentAlarm.Value });
                        }

                        lock (_lock)
                        {
                            alarmRepository.AddRecord(alarmRecord);
                        }
                    }

                    TagRecord tagRecord = new TagRecord { Value = currentValue, Timestamp = DateTime.Now, TagId = tag.Id, HighLimit = tag.HighLimit, LowLimit = tag.LowLimit };
                    lock (_lock)
                    {
                        tagRepository.AddRecord(tagRecord);
                    }
                    lock (_lock)
                    {
                        tagHub.Clients.All.SendAsync("tag", tagRecord);
                        tagHandler.SendDataToClient("tag", tagRecord);
                    }
                }
                Thread.Sleep((int)tag.ScanTime);
            }
        }

    }
}
