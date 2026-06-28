namespace FinFlow.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int Id);
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Query();
        Task<int> SaveChangesAsync();
    }
}
