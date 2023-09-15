using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SA_Project.AuthService;
using SA_Project.Data;
using SA_Project.Models;
using SA_Project.Models.Dtos;
using System.Net;

namespace SA_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly APIResponse _apiResponse;
        private readonly ApplicationDbContext _db;

        public AuthController(IAuthService authService, ApplicationDbContext db)
        {
            _authService = authService;
            _apiResponse = new APIResponse();
            _db = db;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Login(LoginRequestDto dto) 
        {
            try
            {
               LoginResponseDto responseDto = await _authService.Login(dto);
                if (responseDto.User == null || responseDto.Token == null) 
                {
                    return BadRequest();
                }

                _apiResponse.statusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                _apiResponse.Result = responseDto;
                return _apiResponse;
            }
            catch (Exception ex) 
            {
                _apiResponse.statusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = ex.Message;
                return _apiResponse;
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> Register(RegisterRequestDto dto) 
        {
            try
            {
                UserDto userDto = await _authService.Register(dto);
                if (userDto == null)
                {
                    return BadRequest();
                }

                _apiResponse.statusCode = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                _apiResponse.Result = userDto;
                return _apiResponse;
            }
            catch (Exception ex)
            {
                _apiResponse.statusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = ex.Message;
                return _apiResponse;
            }
        }

        [Authorize(Roles ="admin")]
        [HttpPost("assignRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> AssignRole(string email , string role)
        {

            ApplicationUser? user = await _db.ApplicationUsers.FirstOrDefaultAsync(x=>x.Email!.ToLower() == email.ToLower());

            if (user == null) 
            {
                return BadRequest();
            }

            bool roleIsAssigned = await _authService.AssignRole(email, role.ToLower());
            if (!roleIsAssigned)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = "Error Occured";
                return BadRequest(_apiResponse);
            }
            _apiResponse.IsSuccess = true;
            return Ok(_apiResponse);
        }
    }
}
