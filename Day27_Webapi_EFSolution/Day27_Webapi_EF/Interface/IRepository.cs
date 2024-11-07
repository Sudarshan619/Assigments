using Day27_Webapi_EF.Models;

namespace Day27_Webapi_EF.Interface
{
    public interface IRepository<K,T> where T : class
    {
        Task<T> Add(T entity);

        Task<T> Update(K id,T entity);

        Task<T> Delete(K id);

        Task<T> Get(K id);

        Task<IEnumerable<T>> GetAll();
    }
}
