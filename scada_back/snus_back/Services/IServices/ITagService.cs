using scada_back.DTOs;

namespace scada_back.Services.IServices
{
    public interface ITagService
    {
        public List<TagDTO> getAllTags();
        public ICollection<TagDTO> getAllTagsByIOAddress(string adress);
        public ICollection<ITDbManagerDTO> GetAllInputTagsDBManager();
        public ICollection<OTDbManagerDTO> GetAllOutputTagsDBManager();
        public void CreateTag(CreateTagDTO createTagDTO);
        void DeleteTag(int tagId);
        void UpdateTag(UpdateTagDTO updateTagDTO);
        void ToggleIsScanOn(int tagId);
    }
}
