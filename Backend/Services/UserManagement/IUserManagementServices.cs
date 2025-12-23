using Backend.DTOs.Auth;
using Backend.DTOs;

namespace Backend.Services.UserManagement;

public interface IUserManagementServices
{
    Task CreateUserAsync(RegisterDTO user);
}