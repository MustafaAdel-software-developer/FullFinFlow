using FinFlow.Application.Interfaces;
using FinFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinFlow.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly FinFlowDbContext _dbContext;

        public UserRepository(FinFlowDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ActiveUserAsync(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.IsActive = true;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeActiveUserAsync(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.IsActive = false;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistByEmailAsync(string email)
        {
            var normalizedEmail = email.Trim().ToLowerInvariant();
            return await _dbContext.Users.AnyAsync(u =>
                u.Email.ToLower() == normalizedEmail && !u.IsDeleted);
        }

        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            return await _dbContext.Users.Where(u => u.IsActive == true).ToListAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var normalizedEmail = email.Trim().ToLowerInvariant();
            return await _dbContext.Users.FirstOrDefaultAsync(u =>
                u.Email.ToLower() == normalizedEmail && !u.IsDeleted);
        }

        public async Task<IEnumerable<User>> GetInActiveUsersAsync()
        {
            return await _dbContext.Users.Where(u => u.IsActive == false).ToListAsync();
        }

        public async Task<User> GetUserWithProfilesAsync(int id)
        {
            return await _dbContext.Users
                .Where(u => u.Id == id)
                .Include(u => u.UserProfiles)
                .FirstOrDefaultAsync();
        }

        public async Task RestoreUserAsync(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user != null)
            {
                user.IsDeleted = false;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
