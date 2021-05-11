using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.Repositories;
using Persistence.Settings;

namespace Persistence.Services
{
    public class TagService : BaseService<Tag>, ITagService
    {   
        public TagService(IBaseRepository<Tag> repository, ILoggedInUserService loggedInUserService, ITagsDatabaseSettings settings)
            : base(repository, loggedInUserService, settings)
        {

        }

        public Task<Tag> CreateTag(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
