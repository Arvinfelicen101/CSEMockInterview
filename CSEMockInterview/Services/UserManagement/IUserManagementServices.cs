using CSEMockInterview.DTOs;
using CSEMockInterview.DTOs.Auth;

namespace CSEMockInterview.Services.UserManagement;

public interface IUserManagementServices
{
    Task<APIResponseDTO<String>> CreateUserService(RegisterDTO user);
}