namespace Application.Contracts.Persistence
{
    using System.Threading.Tasks;
    using Domain.Entities;

    public interface ITagService : IBaseService<Tag>
    {
        Task<Tag> CreateTag(Tag tag);
    }
}
