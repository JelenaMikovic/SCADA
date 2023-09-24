using scada_back.DTOs;
using scada_back.Models;
using scada_back.Repositories;
using scada_back.Services.IServices;
using Type = scada_back.Models.Type;

namespace scada_back.Services
{
    public class AlarmService : IAlarmService
    {
        public AlarmRepository alarmRepository;

        public AlarmService(AlarmRepository alarmRepository)
        {
            this.alarmRepository = alarmRepository;
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
        }

        public void DeleteAlarm(int id)
        {
            try
            {
                alarmRepository.DeleteAlarm(id);
            } catch
            {
                throw new Exception();
            }
        }

        public List<AlarmDTO> GetTagsAlarms(int id) {
            List<AlarmDTO> alarms = new List<AlarmDTO>();
            foreach(Alarm alarm in this.alarmRepository.GetByTagId(id))
            {
                alarms.Add(new AlarmDTO { Id = alarm.Id, Priority = alarm.Priority.ToString(), TagId = alarm.TagId, Type = alarm.Type.ToString(), Value = alarm.Value });
                Console.WriteLine(alarm.Value);
            }
            return alarms;
        }
    }
}
