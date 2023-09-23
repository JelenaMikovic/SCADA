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
        
        public TagService(TagRepository tagRepository) {
            this.tagRepository = tagRepository;
        }

        public void CreateTag(CreateTagDTO createTagDTO)
        {
            if (createTagDTO.Type == 1) {
                Tag tag = new Tag
                {
                    Name = createTagDTO.Name,
                    Description = createTagDTO.Description,
                    IOAddress = createTagDTO.IOAddress,
                    Value = createTagDTO.Value,
                };
                this.tagRepository.AddTag(tag);
            }
            else if (createTagDTO.Type == 2) {
                AOTag tag = new AOTag
                {
                    Name = createTagDTO.Name,
                    Description = createTagDTO.Description,
                    IOAddress = createTagDTO.IOAddress,
                    Value = createTagDTO.Value,
                    LowLimit = (double)createTagDTO.LowLimit,
                    HighLimit = (double)createTagDTO.HighLimit,
                    Unit = createTagDTO.Unit
                };
                this.tagRepository.AddAOTag(tag);
            }
            else if(createTagDTO.Type == 3) {
                DITag tag = new DITag
                {
                    Name = createTagDTO.Name,
                    Description = createTagDTO.Description,
                    IOAddress = createTagDTO.IOAddress,
                    Value = createTagDTO.Value,
                    ScanTime = (int)createTagDTO.ScanTime,
                    IsScanOn = (bool)createTagDTO.IsScanOn
                };
                this.tagRepository.AddDITag(tag);
            }
            else if (createTagDTO.Type == 4) {
                AITag tag = new AITag
                {
                    Name = createTagDTO.Name,
                    Description = createTagDTO.Description,
                    IOAddress = createTagDTO.IOAddress,
                    Value = createTagDTO.Value,
                    ScanTime = (int)createTagDTO.ScanTime,
                    IsScanOn = (bool)createTagDTO.IsScanOn,
                    LowLimit = (double)createTagDTO.LowLimit,
                    HighLimit = (double)createTagDTO.HighLimit,
                    Unit = createTagDTO.Unit
                };
                this.tagRepository.AddAITag(tag);
            }
            else
            {
                throw new Exception();
            }
        }

        public void DeleteTag(int tagId)
        {
            try { this.tagRepository.DeleteTag(tagId); }
            catch { throw new Exception(); }
            
        }

        public void UpdateTag(UpdateTagDTO createTagDTO)
        {
            if (createTagDTO.Type == 1)
            {
                Tag tag = new Tag
                {
                    Id = createTagDTO.Id,
                    Name = createTagDTO.Name,
                    Description = createTagDTO.Description,
                    IOAddress = createTagDTO.IOAddress,
                    Value = createTagDTO.Value,
                };
                this.tagRepository.UpdateTag(tag);
            }
            else if (createTagDTO.Type == 2)
            {
                AOTag tag = new AOTag
                {
                    Id = createTagDTO.Id,
                    Name = createTagDTO.Name,
                    Description = createTagDTO.Description,
                    IOAddress = createTagDTO.IOAddress,
                    Value = createTagDTO.Value,
                    LowLimit = (double)createTagDTO.LowLimit,
                    HighLimit = (double)createTagDTO.HighLimit,
                    Unit = createTagDTO.Unit
                };
                this.tagRepository.UpdateAOTag(tag);
            }
            else if (createTagDTO.Type == 3)
            {
                DITag tag = new DITag
                {
                    Id = createTagDTO.Id,
                    Name = createTagDTO.Name,
                    Description = createTagDTO.Description,
                    IOAddress = createTagDTO.IOAddress,
                    Value = createTagDTO.Value,
                    ScanTime = (int)createTagDTO.ScanTime,
                    IsScanOn = (bool)createTagDTO.IsScanOn
                };
                this.tagRepository.UpdateDITag(tag);
            }
            else if (createTagDTO.Type == 4)
            {
                AITag tag = new AITag
                {
                    Id = createTagDTO.Id,
                    Name = createTagDTO.Name,
                    Description = createTagDTO.Description,
                    IOAddress = createTagDTO.IOAddress,
                    Value = createTagDTO.Value,
                    ScanTime = (int)createTagDTO.ScanTime,
                    IsScanOn = (bool)createTagDTO.IsScanOn,
                    LowLimit = (double)createTagDTO.LowLimit,
                    HighLimit = (double)createTagDTO.HighLimit,
                    Unit = createTagDTO.Unit
                };
                this.tagRepository.UpdateAITag(tag);
            }
            else
            {
                throw new Exception();
            }
        }

        public ICollection<ITDbManagerDTO> GetAllInputTagsDBManager()
        {
            throw new NotImplementedException();
        }

        public ICollection<OTDbManagerDTO> GetAllOutputTagsDBManager()
        {
            throw new NotImplementedException();
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
                    ScanTime = null,
                    IsScanOn = null,
                    LowLimit = null,
                    HighLimit = null,
                    Unit = null
                });
            }
            foreach (AITag tag in tagRepository.GetAllAITags())
            {
                tags.Add(new TagDTO
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Description = tag.Description,
                    IOAddress = tag.IOAddress,
                    Value = tag.Value,
                    ScanTime = (int)tag.ScanTime,
                    IsScanOn = (bool)tag.IsScanOn,
                    LowLimit = (double)tag.LowLimit,
                    HighLimit = (double)tag.HighLimit,
                    Unit = tag.Unit
                });
            }
            foreach (DITag tag in tagRepository.GetAllDITags())
            {
                tags.Add(new TagDTO
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Description = tag.Description,
                    IOAddress = tag.IOAddress,
                    Value = tag.Value,
                    ScanTime = (int)tag.ScanTime,
                    IsScanOn = (bool)tag.IsScanOn,
                    LowLimit = null,
                    HighLimit = null,
                    Unit = null
                });
            }
            foreach (AOTag tag in tagRepository.GetAllAOTags())
            {
                tags.Add(new TagDTO
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Description = tag.Description,
                    IOAddress = tag.IOAddress,
                    Value = tag.Value,
                    ScanTime = null,
                    IsScanOn = null,
                    LowLimit = (double)tag.LowLimit,
                    HighLimit = (double)tag.HighLimit,
                    Unit = tag.Unit
                }); ;
            }
            return tags;
        }

        public ICollection<TagDTO> getAllTagsByIOAddress(string adress)
        {
            throw new NotImplementedException();
        }

        public void ToggleIsScanOn(int type, int tagId)
        {
            if (type == 3)
            {
                DITag tag = tagRepository.GetDITagById(tagId);
                if (tag != null)
                {
                    tag.IsScanOn = !tag.IsScanOn;
                    tagRepository.UpdateDITag(tag);
                }
                else
                {
                    throw new Exception();
                }
            }
            else if (type == 4)
            {
                AITag tag = tagRepository.GetAITagById(tagId);
                if (tag != null)
                {
                    tag.IsScanOn = !tag.IsScanOn;
                    tagRepository.UpdateAITag(tag);
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
