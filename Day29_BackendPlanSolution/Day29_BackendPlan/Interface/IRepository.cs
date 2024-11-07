namespace Day29_BackendPlan.Interface
{
    public interface IRepository<K,T> where T : class
    {
        Task<T> Add(T entity);

        Task<T> Update(K id, T entity);

        Task<T> Delete(K id);

        Task<T> Get(K id);

        Task<IEnumerable<T>> GetAll();
    }
}
