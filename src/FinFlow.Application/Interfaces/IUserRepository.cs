using FinFlow.Domain.Entities;

namespace FinFlow.Application.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<bool> ExistByEmailAsync(string email);
        Task<IEnumerable<User>> GetActiveUsersAsync();
        Task<IEnumerable<User>> GetInActiveUsersAsync();
        Task ActiveUserAsync(int userId);
        Task DeActiveUserAsync(int userId);
        Task<User> GetUserWithProfilesAsync(int id);
        Task RestoreUserAsync(int userId);
    }
}
