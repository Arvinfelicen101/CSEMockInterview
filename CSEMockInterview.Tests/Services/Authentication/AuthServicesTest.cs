using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using CSEMockInterview.Services.Authentication;
using CSEMockInterview.Repository.Auth;
using CSEMockInterview.Models;
using CSEMockInterview.DTOs.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSEMockInterview.Tests.Services.Authentication
{
    public class AuthServicesTests
    {
        private readonly Mock<IAuthRepository> _authRepoMock;
        private readonly Mock<UserManager<Users>> _userManagerMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly AuthServices _authService;

        public AuthServicesTests()
        {
            _authRepoMock = new Mock<IAuthRepository>();

            _userManagerMock = new Mock<UserManager<Users>>(
                Mock.Of<IUserStore<Users>>(), null, null, null, null, null, null, null, null);

            _configMock = new Mock<IConfiguration>();

            // Fake JWT config
            _configMock.Setup(c => c["JwtConfig:Key"])
                       .Returns("THIS_IS_A_SUPER_SECRET_KEY_123456");

            _configMock.Setup(c => c["JwtConfig:Issuer"])
                       .Returns("http://localhost");

            _configMock.Setup(c => c["JwtConfig:Audience"])
                       .Returns("http://localhost");

            _configMock.Setup(c => c["JwtConfig:ExpireMinutes"])
                       .Returns("60");

            _authService = new AuthServices(
                _authRepoMock.Object,
                _userManagerMock.Object,
                _configMock.Object
            );
        }

        [Fact]
        public async Task CheckUserService_InvalidUser_ThrowsException()
        {
            //  ARRANGE
            var loginDto = new LoginDTO
            {
                username = "wrong",
                password = "wrong"
            };

            _authRepoMock
                .Setup(r => r.CheckUserRepository(It.IsAny<Users>()))
                .ReturnsAsync((Users)null);

            //  ACT & ASSERT
            var exception = await Assert.ThrowsAsync<Exception>(
                () => _authService.CheckUserService(loginDto)
            );

            Assert.Equal("User does not exists!", exception.Message);
        }

        [Fact]
        public async Task CheckUserService_ValidUser_ReturnsTokens()
        {
            //  ARRANGE 
            var loginDto = new LoginDTO
            {
                username = "testuser",
                password = "password"
            };

            var user = new Users
            {
                Id = "1",
                UserName = "testuser"
            };

            _authRepoMock
                .Setup(r => r.CheckUserRepository(It.IsAny<Users>()))
                .ReturnsAsync(user);

            _userManagerMock
                .Setup(m => m.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "User" });

            // ACT 
            var result = await _authService.CheckUserService(loginDto);

            //  ASSERT 
            Assert.NotNull(result);
            Assert.False(string.IsNullOrWhiteSpace(result.AccessToken));
            Assert.False(string.IsNullOrWhiteSpace(result.RefreshToken));
        }
    }
}
