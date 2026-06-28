using FinFlow.Domain.Entities;

namespace FinFlow.Application.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
    }
}
