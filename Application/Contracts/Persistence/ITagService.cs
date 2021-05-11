namespace Application.Contracts.Persistence
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBaseService<T> where T : class
    {
        Task<T> Get(string id);

        Task<List<T>> Get();

        Task<T> Create(T entity);
        
        Task Update(string id, T entity);

        Task Remove(string id);
    }
}
