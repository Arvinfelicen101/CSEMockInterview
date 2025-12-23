using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Repository.UserManagement;

public interface IUserManagementRepository
{
    Task CreateUserAsync(Users user, string password);
}