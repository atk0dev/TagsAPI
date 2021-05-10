namespace Persistence.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Contracts;
    using Application.Contracts.Persistence;
    using Domain.Common;
    using Domain.Entities;
    using MongoDB.Driver;
    using Persistence.Repositories;
    using Persistence.Settings;
    
    public class BaseService<T> : IBaseService<T> where T : class, IEntity, ITrackableEntity
    {
        private readonly IBaseRepository<T> _repository;
        private readonly ILoggedInUserService _loggedInUserService;

        public BaseService(IBaseRepository<T> repository, ILoggedInUserService loggedInUserService, ITagsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            
            this._loggedInUserService = loggedInUserService;

            this._repository = repository;
            this._repository.SetCollection(database.GetCollection<T>(settings.TagsCollectionName));
        }

        public async Task<List<T>> Get()
        {
            return await this._repository.Get();
        }

        public async Task<T> Get(string id)
        {
            return await this._repository.Get(id);
        }

        public async Task<T> Create(T item)
        {
            var now = DateTime.UtcNow;
            var user = this._loggedInUserService.UserId;

            item.CreatedBy = user;
            item.CreatedDate = now;
            item.LastModifiedBy = user;
            item.LastModifiedDate = now;

            await this._repository.Create(item);
            return item;
        }

        public async Task Update(string id, T item)
        {
            var now = DateTime.UtcNow;
            var user = this._loggedInUserService.UserId;

            item.LastModifiedBy = user;
            item.LastModifiedDate = now;

            await this._repository.Update(id, item);
        }

        public async Task Remove(string id)
        {
            await this._repository.Remove(id);
        }
    }
}
