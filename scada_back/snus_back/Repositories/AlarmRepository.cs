using Microsoft.EntityFrameworkCore;
using scada_back.Database;
using scada_back.Models;
using System.Security.Claims;

namespace scada_back.Repositories
{
    public class AlarmRepository
    {
        private DatabaseContext dbContext;

        public AlarmRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Alarm GetByAlarmId(int alarmId)
        {
            return dbContext.Alarms.FirstOrDefault(t => t.Id == alarmId);
        }

        public List<Alarm> GetByTagId(int tagId)
        {
            return dbContext.Alarms.Where(t => t.TagId == tagId).ToList();
        }

        public void AddAlarm(Alarm alarm)
        {
            this.dbContext.Alarms.Add(alarm);
            this.dbContext.SaveChanges();
        }

        public void UpdateAlarm(Alarm alarm)
        {
            dbContext.Entry(alarm).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void DeleteAlarm(int id)
        {
            var alarm = dbContext.Alarms.FirstOrDefault(t => t.Id == id);
            if (alarm != null)
            {
                dbContext.Alarms.Remove(alarm);
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception();
            }
        }

        public void AddRecord(AlarmRecord alarmRecord)
        {
            this.dbContext.AlarmRecords.Add(alarmRecord);
            this.dbContext.SaveChanges();
        }
    }
}
