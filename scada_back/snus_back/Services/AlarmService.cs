using scada_back.DTOs;
using scada_back.Models;
using scada_back.Repositories;
using scada_back.Services.IServices;
using System.Security.Claims;
using Type = scada_back.Models.Type;

namespace scada_back.Services
{
    public class AlarmService : IAlarmService
    {
        public AlarmRepository alarmRepository;
        public ScanService scanService;
       

        public AlarmService(AlarmRepository alarmRepository,ScanService scanService)
        {
            this.alarmRepository = alarmRepository;
            this.scanService = scanService;
        }


        public List<AlarmRecord> GetAlarmRecords()
        {
            return this.alarmRepository.GetAlarmRecords();
        }

        public void AddAlarm(AddAlarmDTO alarmDTO)
        {
            Alarm alarm = new Alarm
            {
                TagId = alarmDTO.TagId,
                Value = alarmDTO.Value,
                Priority = (Priority)Enum.Parse(typeof(Priority), alarmDTO.Priority),
                Type = (Type)Enum.Parse(typeof(Type), alarmDTO.Type)
            };
            alarmRepository.AddAlarm(alarm);
            scanService.AddNewAlarm(alarm);
        }

        public void DeleteAlarm(int id)
        {
            try
            {
                alarmRepository.DeleteAlarm(id);
                scanService.RemoveAlarm(id);
            } catch
            {
                throw new Exception();
            }

        }

        public List<AlarmDTO> GetTagsAlarms(int id) {
            Console.WriteLine("1");
            List<AlarmDTO> alarmsDTO = new List<AlarmDTO>();
            List<Alarm> alarms;
            alarms = this.alarmRepository.GetByTagId(id);
            Console.WriteLine(alarms);
            foreach (Alarm alarm in alarms)
            {
                alarmsDTO.Add(new AlarmDTO { Id = alarm.Id, Priority = alarm.Priority.ToString(), TagId = alarm.TagId, Type = alarm.Type.ToString(), Value = alarm.Value });
                Console.WriteLine(alarm.Value);
            }
            return alarmsDTO;
        }

        public ICollection<AlarmRecordDTO> GetAlarmsByPriority(string priorityString)
        {
            Priority priority = (Priority)Enum.Parse(typeof(Priority), priorityString.ToUpper());
            ICollection<AlarmRecordDTO> alarmRecordsDTO = new List<AlarmRecordDTO>();

            foreach (AlarmRecord record in alarmRepository.GetAlarmRecords())
            {
                Alarm alarm = alarmRepository.GetByAlarmId(record.AlarmId);
                if (alarm.Priority == priority)
                { alarmRecordsDTO.Add(new AlarmRecordDTO { AlarmId = record.AlarmId ,Priority = priority, TimeStamp = record.Timestamp, TagId = alarm.TagId, Type = alarm.Type, Value = alarm.Value }); }
            }

            return alarmRecordsDTO;
        }

        public ICollection<AlarmRecordDTO> GetAlarmsBetweenDates(DateTime startTime, DateTime endTime)
        {
            ICollection<AlarmRecordDTO> alarmRecordsDTO = new List<AlarmRecordDTO>();

            foreach (AlarmRecord record in alarmRepository.GetAlarmRecords())
            {
                if (record.Timestamp <= endTime && record.Timestamp >= startTime)
                { Alarm alarm = alarmRepository.GetByAlarmId(record.AlarmId); 
                  alarmRecordsDTO.Add(new AlarmRecordDTO { AlarmId = record.AlarmId, Priority = alarm.Priority, TimeStamp = record.Timestamp, TagId = alarm.TagId, Type = alarm.Type, Value = alarm.Value }); }
            }

            return alarmRecordsDTO;
        }
    }
}
