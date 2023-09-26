using Microsoft.EntityFrameworkCore;
using scada_back.Database;
using scada_back.Models;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace scada_back.Repositories
{
    public class TagRepository
    {
        private DatabaseContext dbContext;

        public TagRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Tag> GetAllTags()
        {
            return dbContext.Tags.ToList();
        }

        public Tag GetTagById(int id)
        {
            return dbContext.Tags.FirstOrDefault(t => t.Id == id);
        }

        public void AddTag(Tag tag)
        {
            dbContext.Tags.Add(tag);
            dbContext.SaveChanges();
        }

        public void UpdateTag(Tag tag)
        {
            dbContext.Entry(tag).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void DeleteTag(int id)
        {
            var tag = dbContext.Tags.FirstOrDefault(t => t.Id == id);
            if (tag != null)
            {
                dbContext.Tags.Remove(tag);
                dbContext.SaveChanges();
            } else {
                throw new Exception(); 
            }
           
        }

        public Tag GetByIOAddress(string iOAddress)
        {
            return dbContext.Tags.FirstOrDefault(t => t.IOAddress == iOAddress);
        }

        public void AddRecord(TagRecord tagRecord)
        {
            this.dbContext.TagRecords.Add(tagRecord);
            this.dbContext.SaveChanges();
        }

        public void UpdateAllTags(List<Tag> tags)
        {
            foreach(Tag tag in tags)
            {
                dbContext.Entry(tag).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public void InsertAllTagRecords(List<TagRecord> tagRecords)
        {
            foreach(TagRecord record in tagRecords)
            {
                this.dbContext.TagRecords.Add(record);
                this.dbContext.SaveChanges();
            }
        }
    }
}
