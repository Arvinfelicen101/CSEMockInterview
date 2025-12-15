using CSEMockInterview.DTOs;
using CSEMockInterview.DTOs.Auth;
using CSEMockInterview.Models;
using CSEMockInterview.Repository;
using Microsoft.AspNetCore.Identity;

namespace CSEMockInterview.Services
{
    public class UsermanagementServices
    {
        private readonly UserManagementRepository _repository;
        private readonly UserManager<Users> _manager;
        private readonly IConfiguration _config;

        public UsermanagementServices(UserManagementRepository repository, UserManager<Users> manager, IConfiguration config)
        {
            _repository = repository;
            _manager = manager;
            _config = config;
        }

        public async Task<APIResponseDTO<String>> UserInfo(RegisterDTO user)
        {
            try
            {
                var userInfo = new Users()
                {
                    UserName = user.username,
                    Email = user.email,
                    FirstName = user.firstName,
                    MiddleName = user.middleName,
                    LastName = user.lastName,

                };

                var identityResult = await _repository.CreateUserAsync(userInfo, user.password);

                if (!identityResult.Succeeded)
                {
                    return new APIResponseDTO<string>
                    {
                        success = false,
                        StatusCode = 400,
                        message = "User creation failed",
                        Errors = identityResult.Errors.Select(e => e.Description).ToList()
                    };
                }

                return new APIResponseDTO<string>
                {
                    success = true,
                    StatusCode = 200,
                    message = "User created successfully",
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new APIResponseDTO<string>
                {
                    success = false,
                    StatusCode = 500,
                    message = "Something went wrong",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}

