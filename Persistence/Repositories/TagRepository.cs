namespace Persistence.Repositories
{
    using Domain.Entities;

    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository() : base()
        {
        }
    }
}
