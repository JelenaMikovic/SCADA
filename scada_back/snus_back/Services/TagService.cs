using scada_back.Repositories;
using scada_back.Services.IServices;

namespace scada_back.Services
{
    public class TagService: ITagService
    {
        public TagRepository tagRepository;

        public TagService(TagRepository tagRepository) {
            this.tagRepository = tagRepository;
        }
    }
}
