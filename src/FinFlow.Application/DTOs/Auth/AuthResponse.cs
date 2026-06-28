namespace FinFlow.Application.DTOs.Auth
{
    public class AuthResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
