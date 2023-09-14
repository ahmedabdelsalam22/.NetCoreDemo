using SA_Project.AuthService;
using SA_Project.Models;

namespace SA_Project.AuthService
{
    public class AuthService : IAuthService
    {
        public Task<LoginResponseDto> Login(LoginRequestDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> Register(RegisterRequestDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
