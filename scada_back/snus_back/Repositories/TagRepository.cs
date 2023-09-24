using Microsoft.EntityFrameworkCore;
using scada_back.Database;
using scada_back.Models;
using System.Data.Entity;
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


        // DIGITAL OUTPUT - TAG   
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
        
    }
}
