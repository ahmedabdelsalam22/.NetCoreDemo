using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SA_Project.AuthService;
using SA_Project.Data;
using SA_Project.Models;
using SA_Project.Models.Dtos;
using SA_Project.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SA_Project.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        public AuthService(IMapper mapper, UserManager<ApplicationUser> userManager,ApplicationDbContext db,
            RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _db = db;
            _roleManager = roleManager;
        }

        public async Task<bool> AssignRole(string email, string role)
        {
            ApplicationUser? user = await _db.ApplicationUsers.FirstOrDefaultAsync(x=>x.Email!.ToLower() == email.ToLower());

            if (user == null) 
            {
                return false;
            }

            if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult()) 
            {
                _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
            }
            var IsRoleAdded = await _userManager.AddToRoleAsync(user , role);
            if (!IsRoleAdded.Succeeded) 
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsUniqueUser(string username)
        {
            ApplicationUser? user = await _db.ApplicationUsers.FirstOrDefaultAsync(x=>x.UserName!.ToLower() == username.ToLower());
            if (user == null) 
            {
                return true;
            }
            return false;
        }

        public string JWTGenerateToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(SD.SecretKey!);

            // claims list 

            List<Claim> claims = new()
                {
                    new Claim(JwtRegisteredClaimNames.Name , user.Name),
                    new Claim(JwtRegisteredClaimNames.Email , user.Email!),
                    new Claim(JwtRegisteredClaimNames.Sub , user.Id),
                };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto dto)
        {
            bool isUnique = await IsUniqueUser(dto.Username);

            if(isUnique)
            {
                return new LoginResponseDto() 
                {
                    User = null,
                    Token = ""
                };
            }

            ApplicationUser user = await _db.ApplicationUsers.FirstAsync(x=>x.UserName!.ToLower() == dto.Username.ToLower());


            bool IsPasswordCorrect = await _userManager.CheckPasswordAsync(user,dto.Password);
            if (!IsPasswordCorrect) 
            {
                return new LoginResponseDto() 
                {
                    User = null,
                    Token = null
                };
            }

            // generate token 

            string token = JWTGenerateToken(user);

            UserDto userDto = _mapper.Map<UserDto>(user);

            return new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };

        }

        public async Task<UserDto> Register(RegisterRequestDto dto)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(dto);

            var result = await _userManager.CreateAsync(user , dto.Password);

            if (result.Succeeded)
            {
                // assign role

                await AssignRole(dto.Email , SD.CUSTOMER);

                ApplicationUser userFromDb = _db.ApplicationUsers.First(x=>x.UserName!.ToLower() == dto.Username.ToLower());

                UserDto userDto = _mapper.Map<UserDto>(userFromDb);

                return userDto;
            }
            return new UserDto();
        }
    }
}
