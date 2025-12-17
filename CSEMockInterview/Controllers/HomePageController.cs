using CSEMockInterview.DTOs.Auth;
using CSEMockInterview.Repository;
using CSEMockInterview.Services;
using CSEMockInterview.Services.Authentication;
using CSEMockInterview.Services.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSEMockInterview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageController : ControllerBase
    {
        private readonly IAuthServices _service;
        private readonly IUserManagementServices _userManagement;

        public HomePageController(IAuthServices service, IUserManagementServices userManagement)
        {
            _service = service;
            _userManagement = userManagement;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            var result = await _service.CheckUserService(user);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO user)
        {
            await _userManagement.CreateUserAsync(user);
            return Ok(new { message = "User created successfully" });
        }


    }
}
