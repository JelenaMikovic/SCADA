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

        // DIGITAL INPUT - DITAG
        public List<DITag> GetAllDITags()
        {
            return dbContext.Tags.OfType<DITag>().ToList();
        }

        public DITag GetDITagById(int id)
        {
            return dbContext.Tags.OfType<DITag>().FirstOrDefault(t => t.Id == id);
        }

        public void AddDITag(DITag diTag)
        {
            dbContext.Tags.Add(diTag);
            dbContext.SaveChanges();
        }

        public void UpdateDITag(DITag diTag)
        {
            dbContext.Entry(diTag).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void DeleteDITag(int id)
        {
            var diTag = dbContext.Tags.OfType<DITag>().FirstOrDefault(t => t.Id == id);
            if (diTag != null)
            {
                dbContext.Tags.Remove(diTag);
                dbContext.SaveChanges();
            }
        }

        // ANALOG OUTPUT - AOTAG
        public List<AOTag> GetAllAOTags()
        {
            return dbContext.Tags.OfType<AOTag>().ToList();
        }

        public AOTag GetAOTagById(int id)
        {
            return dbContext.Tags.OfType<AOTag>().FirstOrDefault(t => t.Id == id);
        }

        public void AddAOTag(AOTag aoTag)
        {
            dbContext.Tags.Add(aoTag);
            dbContext.SaveChanges();
        }

        public void UpdateAOTag(AOTag aoTag)
        {
            dbContext.Entry(aoTag).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void DeleteAOTag(int id)
        {
            var aoTag = dbContext.Tags.OfType<AOTag>().FirstOrDefault(t => t.Id == id);
            if (aoTag != null)
            {
                dbContext.Tags.Remove(aoTag);
                dbContext.SaveChanges();
            }
        }

        // ANALOG INPUT - AITAG
        public List<AITag> GetAllAITags()
        {
            return dbContext.Tags.OfType<AITag>().ToList();
        }

        public AITag GetAITagById(int id)
        {
            return dbContext.Tags.OfType<AITag>().FirstOrDefault(t => t.Id == id);
        }

        public void AddAITag(AITag aiTag)
        {
            dbContext.Tags.Add(aiTag);
            dbContext.SaveChanges();
        }

        public void UpdateAITag(AITag aiTag)
        {
            dbContext.Entry(aiTag).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void DeleteAITag(int id)
        {
            var aiTag = dbContext.Tags.OfType<AITag>().FirstOrDefault(t => t.Id == id);
            if (aiTag != null)
            {
                dbContext.Tags.Remove(aiTag);
                dbContext.SaveChanges();
            }
        }
    }
}
