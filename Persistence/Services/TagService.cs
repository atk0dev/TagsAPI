namespace Persistence.Services
{
    using System;
    using System.Threading.Tasks;
    using Application.Contracts;
    using Application.Contracts.Persistence;
    using MongoDB.Driver;
    using Persistence.Repositories;
    using Persistence.Settings;

    public class TagService : BaseService<Domain.Entities.Tag>, ITagService
    {   
        public TagService(
            IMongoClient client,
            IBaseRepository<Domain.Entities.Tag> repository, 
            ILoggedInUserService loggedInUserService, 
            IDatabaseContext context)
            : base(client, repository, loggedInUserService, context)
        {
        }

        public async Task<Domain.Entities.Tag> CreateTag(Domain.Entities.Tag tag)
        {
            tag.TagID = Guid.NewGuid().ToString();
            tag.IsArchived = false;

            return await this.Create(tag);
        }
    }
}
