using scada_back.DTOs;
using scada_back.Models;
using scada_back.Repositories;
using scada_back.Services.IServices;
using System;

namespace scada_back.Services
{
    public class TagService: ITagService
    {
        public TagRepository tagRepository;
        public ScanService scanService;
        
        public TagService(TagRepository tagRepository, ScanService scanService) {
            this.tagRepository = tagRepository;
            this.scanService = scanService;
        }

        public void CreateTag(CreateTagDTO createTagDTO)
        {
            Tag tag = new Tag
            {
                Name = createTagDTO.Name,
                Description = createTagDTO.Description,
                IOAddress = createTagDTO.IOAddress,
                Value = createTagDTO.Value,
                ScanTime = createTagDTO.ScanTime.HasValue ? (int?)createTagDTO.ScanTime.Value : null,
                IsScanOn = createTagDTO.IsScanOn.HasValue ? (bool?)createTagDTO.IsScanOn.Value : null,
                LowLimit = createTagDTO.LowLimit.HasValue ? (double?)createTagDTO.LowLimit.Value : null,
                HighLimit = createTagDTO.HighLimit.HasValue ? (double?)createTagDTO.HighLimit.Value : null,
                Unit = createTagDTO.Unit,
                TagType = (TagType)Enum.Parse(typeof(TagType), createTagDTO.Type)
            };
            this.tagRepository.AddTag(tag);
            if(tag.TagType == TagType.DI || tag.TagType == TagType.AI)
            { this.scanService.AddNewTag(tag); }
        }

        public void DeleteTag(int tagId)
        {
            try { 
                this.tagRepository.DeleteTag(tagId);
                scanService.DeleteTag(tagId);
            }
            catch { throw new Exception(); }
            
        }

        public void UpdateTag(UpdateTagDTO createTagDTO)
        {
            Tag tag = tagRepository.GetTagById(createTagDTO.Id);
            tag.Name = createTagDTO.Name;
            tag.Description = createTagDTO.Description;
            tag.IOAddress = createTagDTO.IOAddress;
            tag.Value = createTagDTO.Value;
            tag.ScanTime = createTagDTO.ScanTime.HasValue ? (int?)createTagDTO.ScanTime.Value : null;
            tag.IsScanOn = createTagDTO.IsScanOn.HasValue ? (bool?)createTagDTO.IsScanOn.Value : null;
            tag.LowLimit = createTagDTO.LowLimit.HasValue ? (double?)createTagDTO.LowLimit.Value : null;
            tag.HighLimit = createTagDTO.HighLimit.HasValue ? (double?)createTagDTO.HighLimit.Value : null;
            tag.Unit = createTagDTO.Unit;
            this.tagRepository.UpdateTag(tag);
        }

        public List<TagDTO> getAllTags()
        {
            List<TagDTO> tags = new List<TagDTO>();
            foreach (Tag tag in tagRepository.GetAllTags())
            {
                tags.Add(new TagDTO
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Description = tag.Description,
                    IOAddress = tag.IOAddress,
                    Value = tag.Value,
                    ScanTime = tag.ScanTime.HasValue ? (int?)tag.ScanTime.Value : null, 
                    IsScanOn = tag.IsScanOn.HasValue ? (bool?)tag.IsScanOn.Value : null,  
                    LowLimit = tag.LowLimit.HasValue ? (double?)tag.LowLimit.Value : null,  
                    HighLimit = tag.HighLimit.HasValue ? (double?)tag.HighLimit.Value : null,  
                    Unit = tag.Unit,
                    TagType = tag.TagType.ToString()
                });
            }
            return tags;
        }

        public void ToggleIsScanOn(int tagId)
        {
            Tag tag = tagRepository.GetTagById(tagId);
            if (tag != null)
            {
                tag.IsScanOn = !tag.IsScanOn;
                tagRepository.UpdateTag(tag);
                scanService.ToggleScan(tagId);
            }
            else
            {
                throw new Exception();
            }
        }

        public ICollection<TagRecordDTO> GetAllRecordsByIOAddress(string address)
        {
            ICollection<TagRecordDTO> tagRecordDTOs = new List<TagRecordDTO>();
            foreach(TagRecord record in this.tagRepository.GetAllRecords())
            {
                if(tagRepository.GetTagById(record.TagId).IOAddress == address)
                {
                    tagRecordDTOs.Add(new TagRecordDTO { TagId = record.TagId, Value = record.Value, Timestamp = record.Timestamp, HighLimit = record.HighLimit, LowLimit = record.LowLimit });
                }
            }
            return tagRecordDTOs;
        }

        List<TagDTO> ITagService.getAllTags()
        {
            throw new NotImplementedException();
        }

        ICollection<TagDTO> ITagService.getAllTagsByIOAddress(string adress)
        {
            throw new NotImplementedException();
        }

        void ITagService.CreateTag(CreateTagDTO createTagDTO)
        {
            throw new NotImplementedException();
        }

        void ITagService.DeleteTag(int tagId)
        {
            throw new NotImplementedException();
        }

        void ITagService.UpdateTag(UpdateTagDTO updateTagDTO)
        {
            throw new NotImplementedException();
        }

        void ITagService.ToggleIsScanOn(int tagId)
        {
            throw new NotImplementedException();
        }

        ICollection<TagRecordDTO> ITagService.GetAllRecordsByIOAddress(string address)
        {
            throw new NotImplementedException();
        }
    }
}
