namespace Persistence.Repositories
{
    using System.Threading.Tasks;
    using Domain.Entities;

    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository() : base()
        {
        }

        public async Task<Tag> CreateTag(Tag tag)
        {
            return await this.Create(tag);
        }
    }
}
