using scada_back.DTOs;

namespace scada_back.Services.IServices
{
    public interface ITagService
    {
        public List<TagDTO> getAllTags();
        public void CreateTag(CreateTagDTO createTagDTO);
        void DeleteTag(int tagId);
        void UpdateTag(UpdateTagDTO updateTagDTO);
        void ToggleIsScanOn(int tagId);
        ICollection<TagRecordDTO> GetAllRecordsByIOAddress(string address);
        ICollection<TagRecordDTO> GetAllTagsBetweenDates(DateTime startTime, DateTime endTime);
        ICollection<TagRecordDTO> GetLatestAI();
        ICollection<TagRecordDTO> GetLatestDI();
    }
}
