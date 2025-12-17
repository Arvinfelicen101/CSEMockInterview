using CSEMockInterview.DTOs;
using CSEMockInterview.DTOs.Auth;

namespace CSEMockInterview.Services.UserManagement;

public interface IUserManagementServices
{
    Task CreateUserAsync(RegisterDTO user);
}