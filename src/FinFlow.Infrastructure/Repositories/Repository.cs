using FinFlow.Application.Interfaces;
using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinFlow.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<T> GetByIdAsync(int id) => await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        public IQueryable<T> Query() => _dbSet.AsQueryable();
        public void Update(T entity) => _dbSet.Update(entity);
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
