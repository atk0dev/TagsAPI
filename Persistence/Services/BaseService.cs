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
        private readonly IMongoDatabase _database;

        public BaseService(
            IMongoClient client, 
            IBaseRepository<T> repository, 
            ILoggedInUserService loggedInUserService, 
            IDatabaseContext context)
        {
            this._database = client.GetDatabase(context.DatabaseName);
            
            this._loggedInUserService = loggedInUserService;

            this._repository = repository;
            ////this._repository.SetCollection(this._database.GetCollection<T>(nameof(T)));
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

            item.CreatedByID = user;
            item.CreatedDate = now;
            
            await this._repository.Create(item);
            return item;
        }

        public async Task Update(string id, T item)
        {
            var now = DateTime.UtcNow;
            var user = this._loggedInUserService.UserId;

            item.UpdatedByID = user;
            item.UpdatedDate = now;

            await this._repository.Update(id, item);
        }

        public async Task Remove(string id)
        {
            await this._repository.Remove(id);
        }

        protected void SetCollectionName(string collectionName)
        {
            this._repository.SetCollection(this._database.GetCollection<T>(collectionName));
        }
    }
}
