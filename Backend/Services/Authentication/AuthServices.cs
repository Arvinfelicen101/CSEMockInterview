using Backend.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Backend.DTOs.Auth;
using Backend.Models;
using Backend.Repository.Auth;

namespace Backend.Services.Authentication
{
    public class AuthServices : IAuthServices
    {
        private readonly IAuthRepository _repository;
        private readonly UserManager<Users> _manager;
        private readonly IConfiguration _config;

        public AuthServices(IAuthRepository repository, UserManager<Users> manager, IConfiguration config)
        {
            _repository = repository;
            _manager = manager;
            _config = config;
        }

        public async Task<TokenDTO> CheckUserService(LoginDTO user)
        {
            var userMapping = new Users()
            {
                UserName = user.username,
                PasswordHash = user.password
            };
            var result = await _repository.CheckUserRepository(userMapping);
            if (result == null)
            {
                throw new Exception("User does not exists!");
            }
            var token = await GenerateJwtToken(result);
            return new TokenDTO()
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken
            };
            

        }

        private async Task<TokenDTO> GenerateJwtToken(Users user)
        {
            var userRoles = await _manager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            }
            .Concat(userRoles.Select(role => new Claim(ClaimTypes.Role, role)))
            .ToList();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer: _config["JwtConfig:Issuer"],
              audience: _config["JwtConfig:Audience"],
              claims: claims,
              expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtConfig:ExpireMinutes"])),
              signingCredentials: creds
             );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            return new TokenDTO()
            {
                AccessToken = tokenString,
                RefreshToken = refreshToken
            };
        }
    }
}
    

