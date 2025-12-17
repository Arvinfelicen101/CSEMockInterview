using CSEMockInterview.DTOs.Auth;

namespace CSEMockInterview.Services.Authentication;

public interface IAuthServices
{
    Task<TokenDTO> CheckUserService(LoginDTO user);
}