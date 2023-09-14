using Microsoft.IdentityModel.Tokens;
using SA_Project.Models;
using SA_Project.Models.Dtos;
using SA_Project.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SA_Project.AuthService
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto dto);
        Task<UserDto> Register(RegisterRequestDto dto);
        Task<bool> AssignRole(string email , string role);
        Task<bool> IsUniqueUser(string username);
        string JWTGenerateToken(ApplicationUser user);
    }
}
