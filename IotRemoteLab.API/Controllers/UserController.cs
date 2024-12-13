using Asp.Versioning;
using IotRemoteLab.API.Services;
using IotRemoteLab.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IotRemoteLab.API.Controllers
{
    [Authorize]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController
    {
        private readonly Guid _userId;
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(UserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                _userId = Guid.Parse(
                    httpContextAccessor.HttpContext.User.FindFirst("Id").Value
                    );
            }
        }

        [Authorize(Roles = "Student, Admin")]
        [HttpGet("profile")]
        public async Task<User?> GetUserProfile()
        {
            var user = await _userService.UserProfileAsync(_userId);
            if (user != null)
            {
                user.PasswordHash = null;
            }
            return user;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public Task<List<User>> GetUsers() 
        {
            return _userService.GetUsersAsync();
        }

        [Authorize(Roles = "Student, Admin")]
        [HttpGet("users/byUniversity/{universityId:guid}")]
        public Task<List<User>> GetUsersByUniversity([FromRoute]Guid universityId) 
        {
            return _userService.GetUserByUniversity(universityId);
        }
    }
}
