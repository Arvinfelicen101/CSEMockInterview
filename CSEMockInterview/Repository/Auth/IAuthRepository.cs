using CSEMockInterview.Models;

namespace CSEMockInterview.Repository.Auth;

public interface IAuthRepository
{
    Task<Users?> CheckUserRepository(Users user);
}