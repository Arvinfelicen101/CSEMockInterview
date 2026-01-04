using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Repository.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<Users> manager;
        public AuthRepository(UserManager<Users> userManager)
        {
            manager = userManager;
        }
        public async Task<Users?> CheckUserRepository(Users user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                return null;
            }
            var result = await manager.FindByNameAsync(user.UserName);
            return result;
        }
    }
}
