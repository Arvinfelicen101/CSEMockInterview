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
        private readonly AuthServices _service;
        private readonly IUserManagementServices _userManagement;

        public HomePageController(AuthServices service, UserManagementServices userManagement)
        {
            _service = service;
            _userManagement = userManagement;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            var result = await _service.CheckUserService(user);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO user)
        {
            var result = await _userManagement.CreateUserService(user);
            return StatusCode(result.StatusCode, result);
        }


    }
}
