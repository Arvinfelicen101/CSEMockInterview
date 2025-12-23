using Backend.DTOs.Auth;

namespace Backend.Services.Authentication;

public interface IAuthServices
{
    Task<TokenDTO> CheckUserService(LoginDTO user);
}