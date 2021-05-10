namespace Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Domain.Entities;
    using MongoDB.Driver;

    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        private IMongoCollection<T> _items;

        public void SetCollection(IMongoCollection<T> items)
        {
            this._items = items;
        }

        public async Task<List<T>> Get()
        {
            var result = await this._items.FindAsync(item => true);
            return await result.ToListAsync();
        }

        public async Task<T> Get(string id)
        {
            var result = await this._items.FindAsync<T>(item => item.Id == id);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<T> Create(T item)
        {
            await this._items.InsertOneAsync(item);
            return item;
        }

        public async Task Update(string id, T item)
        {
            await this._items.ReplaceOneAsync(i => i.Id == id, item);
        }

        public async Task Remove(string id)
        {
            await this._items.DeleteOneAsync(item => item.Id == id);
        }
    }
}
