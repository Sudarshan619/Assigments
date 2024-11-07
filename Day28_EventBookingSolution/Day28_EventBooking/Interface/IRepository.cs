using Day28_EventBooking.Model;

namespace Day28_EventBooking.Interface
{
    public interface IRepository<K,T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(K key);

        Task<T> Add(T Entity);

        Task<T> Update(K key,T Entity);

        Task<T> Delete(K key);

    }
}
