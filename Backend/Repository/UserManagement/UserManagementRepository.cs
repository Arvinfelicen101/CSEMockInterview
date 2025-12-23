using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Repository.UserManagement
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly UserManager<Users> _manager;

        public UserManagementRepository(UserManager<Users> manager)
        {
            _manager = manager;
        }
        public async Task CreateUserAsync(Users user, string password)
        {
            await _manager.CreateAsync(user, password);
        }
        
        
    }
}
