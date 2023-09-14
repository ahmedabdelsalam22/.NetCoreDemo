using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SA_Project.AuthService;
using SA_Project.Data;
using SA_Project.Models;
using SA_Project.Models.Dtos;
using SA_Project.Utilities;

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

        public Task<LoginResponseDto> Login(LoginRequestDto dto)
        {
            throw new NotImplementedException();
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
