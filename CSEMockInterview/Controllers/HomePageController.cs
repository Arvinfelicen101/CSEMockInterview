using CSEMockInterview.DTOs.Auth;
using CSEMockInterview.Services.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSEMockInterview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageController : ControllerBase
    {
        private readonly AuthServices _service;

        public HomePageController(AuthServices service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            var result = await _service.CheckUserService(user);
            return StatusCode(result.StatusCode, result);
        }
    }
}
