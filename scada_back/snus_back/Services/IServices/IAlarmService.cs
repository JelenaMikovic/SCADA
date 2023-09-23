using scada_back.DTOs;

namespace scada_back.Services.IServices
{
    public interface IAlarmService
    {
        public void AddAlarm(AddAlarmDTO dto);
        void DeleteAlarm(int id);
        List<AlarmDTO> GetTagsAlarms(int tagId);
    }
}
