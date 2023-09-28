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
        private readonly AlarmHandler alarmHandler;

        private static List<Alarm> alarms = new List<Alarm>();
        private static List<Tag> tags = new List<Tag>();
        private static List<AlarmRecord> alarmRecords = new List<AlarmRecord>();
        private static List<TagRecord> tagRecords = new List<TagRecord>();

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
            foreach (Alarm alarm in alarmRepository.GetAll())
            {
                alarms.Add(alarm);
            }
            foreach (Tag tag in tagRepository.GetAllTags())
            {
                if (tag.TagType.Equals(TagType.AO) || tag.TagType.Equals(TagType.DO)) continue;
                tags.Add(tag);
            }
            foreach (Tag tag in tagRepository.GetAllTags())
            {
                if (tag.TagType.Equals(TagType.AO) || tag.TagType.Equals(TagType.DO)) continue;
                Thread thread = new Thread(() => StartSimulationThread(tag));
                thread.Start();
            }
            Thread thr = new Thread(UpdateDatabase);
            thr.Start();
        }

        public void UpdateDatabase()
        {
            while (true)
            {
                lock (Utils._lock)
                {
                    tagRepository.UpdateAllTags(tags);
                    tagRepository.InsertAllTagRecords(tagRecords);
                    tagRecords.Clear();
                    alarmRepository.InsertAllAlarmRecords(alarmRecords);
                    alarmRecords.Clear();
                    foreach (var tag in tags)
                    {
                        Console.WriteLine("Tag ID: " + tag.Id);
                    }
                }
                Thread.Sleep(3000);
            }

        }
        public void AddNewTag(Tag tag)
        {
            lock (Utils._lock)
            {
                tags.Add(tag);
                Console.WriteLine("Tag added with ID: " + tag.Id);
            }
            Thread thread = new Thread(() => StartSimulationThread(tag));
            thread.Start();
        }


        public void AddNewAlarm(Alarm alarm)
        {
            lock (Utils._lock)
            {
                alarms.Add(alarm);
            }
        }

        public void RemoveAlarm(int id)
        {
            lock (Utils._lock)
            {
                alarms.Remove(alarms.Find(a => a.Id == id));
            }
        }

        public void ToggleScan(int id)
        {
            lock (Utils._lock)
            {
                var tag = tags.Find(t => t.Id == id);
                if (tag != null)
                {
                    Console.WriteLine("Found tag with ID " + id);
                    tag.IsScanOn = !tag.IsScanOn;
                }
                else
                {
                    Console.WriteLine("Tag with ID " + id + " not found.");
                }
            }
        }

        public void DeleteTag(int id)
        {
            lock (Utils._lock)
            {
                tags.Remove(tags.Find(t => t.Id == id));
            }
        }

        public void EditTag(UpdateTagDTO createTagDTO)
        {
            lock (Utils._lock)
            {
                Tag tag = tags.Find(t => t.Id == createTagDTO.Id);
                tag.Name = createTagDTO.Name;
                tag.Description = createTagDTO.Description;
                tag.IOAddress = createTagDTO.IOAddress;
                tag.Value = createTagDTO.Value;
                tag.ScanTime = createTagDTO.ScanTime.HasValue ? (int?)createTagDTO.ScanTime.Value : null;
                tag.IsScanOn = createTagDTO.IsScanOn.HasValue ? (bool?)createTagDTO.IsScanOn.Value : null;
                tag.LowLimit = createTagDTO.LowLimit.HasValue ? (double?)createTagDTO.LowLimit.Value : null;
                tag.HighLimit = createTagDTO.HighLimit.HasValue ? (double?)createTagDTO.HighLimit.Value : null;
                tag.Unit = createTagDTO.Unit;
            }

        }

        private void StartSimulationThread(object obj)
        {
            Tag tag = (Tag)obj;
            double currentValue = -1000;
            Alarm currentAlarm;

            while (true)
            {
                lock (Utils._lock)
                {
                    tag = tags.Find(t => t.Id == tag.Id);
                }

                if (tag == null)
                {
                    break;
                }

                if ((bool)tag.IsScanOn)
                {
                    if (tag.IOAddress == "SIN" || tag.IOAddress == "COS" || tag.IOAddress == "RAMP")
                    {
                        currentValue = SimulationService.CalculateValue(tag.IOAddress);
                        if (tag.TagType.Equals(TagType.DI)) currentValue = (currentValue % 2)*(currentValue % 2);
                    }
                    else
                    {
                        lock (Utils._lock)
                        {
                            currentValue = DeviceRepository.devices[tag.IOAddress];
                        }
                    }
                    currentAlarm = null;
                    List<Alarm> found;
                    lock (Utils._lock)
                    {
                        found = alarms.FindAll(t => t.TagId == tag.Id);
                    }
                    foreach (Alarm alarm in found)
                    {
                        Console.WriteLine(alarm);
                        if ((alarm.Type == Type.HIGHER && currentValue >= alarm.Value) || (alarm.Type == Type.LOWER && currentValue <= alarm.Value))
                        {
                            if (currentAlarm == null || (currentAlarm != null && currentAlarm.Priority < alarm.Priority))
                            {
                                currentAlarm = alarm;
                            }
                        }
                        Console.WriteLine("PROSO");
                    }

                    lock (Utils._lock)
                    {
                        tag.Value = currentValue;
                        tags.Find(t => t.Id == tag.Id).Value = currentValue;
                    }

                    if (currentAlarm != null)
                    {
                        Console.WriteLine("PROSO");
                        if (currentAlarm.Type == Type.HIGHER) { currentValue = (double)tag.HighLimit; }
                        else { currentValue = (double)tag.LowLimit; }

                        AlarmRecord alarmRecord = new AlarmRecord { AlarmId = currentAlarm.Id, Timestamp = DateTime.Now, TagId = tag.Id };

                        lock (Utils._lock)
                        {
                            AlarmRecordDTO notify = new AlarmRecordDTO { TagId = tag.Id, Priority = currentAlarm.Priority, Type = currentAlarm.Type, Value = currentAlarm.Value, TimeStamp = DateTime.Now };
                            alarmHub.Clients.All.SendAsync("alarm", notify);
                            //alarmHandler.SendDataToClient("alarm", notify);
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
