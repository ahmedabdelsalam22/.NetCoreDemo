using SA_Project.Models;

namespace SA_Project.AuthService.IAuthService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto dto);
        Task<ApplicationUser> Register(RegisterRequestDto dto);
    }
}
