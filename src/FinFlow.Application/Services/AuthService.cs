using FinFlow.Application.DTOs.Auth;
using FinFlow.Application.Interfaces;
using FinFlow.Domain.Entities;
using FinFlow.Domain.Enums;

namespace FinFlow.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            ValidateRegisterRequest(request);

            if (await _userRepository.ExistByEmailAsync(request.Email))
            {
                throw new InvalidOperationException("An account with this email already exists.");
            }

            var user = new User
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Email = request.Email.Trim().ToLowerInvariant(),
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                IsActive = true,
                IsDeleted = false,
                UserProfiles = new List<UserProfile>
                {
                    new()
                    {
                        Address = request.AddressLine?.Trim() ?? string.Empty,
                        City = request.City?.Trim() ?? string.Empty,
                        ZipCode = request.ZipCode?.Trim() ?? string.Empty,
                        Country = string.IsNullOrWhiteSpace(request.Country) ? "Egypt" : request.Country.Trim(),
                        RiskTolerance = RiskTolerances.Moderate,
                        FinancialExperience = FinancialExperiences.Beginner
                    }
                }
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return await BuildAuthResponseAsync(user, "Registration successful.");
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                throw new InvalidOperationException("Email and password are required.");
            }

            var user = await _userRepository.GetByEmailAsync(request.Email.Trim().ToLowerInvariant());
            if (user == null || user.IsDeleted || !user.IsActive)
            {
                throw new InvalidOperationException("Invalid email or password.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                throw new InvalidOperationException("Invalid email or password.");
            }

            return await BuildAuthResponseAsync(user, "Login successful.");
        }

        private async Task<AuthResponse> BuildAuthResponseAsync(User user, string message)
        {
            var refreshToken = _jwtService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.SpecifyKind(DateTime.UtcNow.AddDays(7), DateTimeKind.Unspecified);
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return new AuthResponse
            {
                Message = message,
                Token = _jwtService.GenerateToken(user),
                RefreshToken = refreshToken
            };
        }

        private static void ValidateRegisterRequest(RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.LastName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                throw new InvalidOperationException("First name, last name, email, and password are required.");
            }

            if (request.Password.Length < 6)
            {
                throw new InvalidOperationException("Password must be at least 6 characters.");
            }

            if (!string.Equals(request.Password, request.ConfirmPassword, StringComparison.Ordinal))
            {
                throw new InvalidOperationException("Password and confirmation password do not match.");
            }
        }
    }
}
