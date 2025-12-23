using Backend.Models;

namespace Backend.Repository.Auth;

public interface IAuthRepository
{
    Task<Users?> CheckUserRepository(Users user);
}