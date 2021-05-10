namespace Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Driver;

    public interface IBaseRepository<T> where T : class
    {
        void SetCollection(IMongoCollection<T> items);

        Task<T> Get(string id);

        Task<List<T>> Get();

        Task<T> Create(T entity);
        
        Task Update(string id, T entity);

        Task Remove(string id);
    }
}
