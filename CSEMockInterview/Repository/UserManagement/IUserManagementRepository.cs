using CSEMockInterview.Models;
using Microsoft.AspNetCore.Identity;

namespace CSEMockInterview.Repository.UserManagement;

public interface IUserManagementRepository
{
    Task CreateUserAsync(Users user, string password);
}