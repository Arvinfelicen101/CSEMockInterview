using CSEMockInterview.Models;
using Microsoft.AspNetCore.Identity;

namespace CSEMockInterview.Repository.UserManagement
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly UserManager<Users> _manager;

        public UserManagementRepository(UserManager<Users> manager)
        {
            _manager = manager;
        }
        public async Task<IdentityResult> CreateUserAsync(Users user, string password)
        {
            return await _manager.CreateAsync(user, password);
        }
    }
}
