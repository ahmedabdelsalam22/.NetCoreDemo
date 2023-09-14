using SA_Project.Models;

namespace SA_Project.AuthService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto dto);
        Task<ApplicationUser> Register(RegisterRequestDto dto);
    }
}
