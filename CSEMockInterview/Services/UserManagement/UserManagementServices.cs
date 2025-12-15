using CSEMockInterview.DTOs;
using CSEMockInterview.DTOs.Auth;
using CSEMockInterview.Models;
using CSEMockInterview.Repository.UserManagement;

namespace CSEMockInterview.Services.UserManagement
{
    public class UserManagementServices : IUserManagementServices
    {
        private readonly IUserManagementRepository _repository;
        
        public UserManagementServices(IUserManagementRepository repository)
        {
            _repository = repository;
        }

        public async Task<APIResponseDTO<String>> CreateUserService(RegisterDTO user)
        {
            if (user.password != user.confirmPassword)
            {
                return new APIResponseDTO<string>()
                {
                    success = false,
                    StatusCode = 404,
                    message = "Password mismatched"
                };
            }
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

