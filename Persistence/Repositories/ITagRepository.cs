namespace Persistence.Repositories
{
    using System.Threading.Tasks;
    using Domain.Entities;

    public interface ITagRepository : IBaseRepository<Tag>
    {
        Task<Tag> CreateTag(Tag tag);
    }
}
