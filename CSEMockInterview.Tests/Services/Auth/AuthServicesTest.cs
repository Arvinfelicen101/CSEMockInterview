using CSEMockInterview.DTOs.Auth;
using CSEMockInterview.Models;
using CSEMockInterview.Repository;
using CSEMockInterview.Services.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEMockInterview.Tests.Services.Auth
{
    public class AuthServicesTest
    {
        private readonly Mock<IAuthRepository> _authRepoMock;
        private readonly Mock<UserManager<Users>> _userManagerMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly AuthServices _authService;

        public AuthServicesTest() 
        {
            _authRepoMock = new Mock<IAuthRepository>();
            _userManagerMock = new Mock<UserManager<Users>>(
                Mock.Of<IUserStore<Users>>(), null, null, null, null, null, null, null, null);

            _configMock = new Mock<IConfiguration>();

            _configMock.Setup(c => c["JwtConfig:Key"])
                    .Returns("ce627f4131dd4bfe133a4c8bc4d7ce2683b97cbb2d13f2fc2df65bdfdfcef0006f815458ffe09d869f03d70f50fafcd571fa116f19b0bc204e70444da4fca272");

            _configMock.Setup(c => c["JwtConfig:Issuer"])
                    .Returns("http://localhost:5140");

            _configMock.Setup(c => c["JwtConfig:Audience"])
                   .Returns("http://localhost:5140");

            _configMock.Setup(c => c["JwtConfig:ExpireMinutes"])
                    .Returns("60");

            _authService = new AuthServices(
             _authRepoMock.Object,
             _userManagerMock.Object,
             _configMock.Object
         );

        }

        [Fact]
        public async Task CheckUserService_ValidUser_ReturnsToken()
        {
            // ARRANGE
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

            _authRepoMock.Setup(r => r.CheckUserRepository(It.IsAny<Users>()))
                    .ReturnsAsync(user);

            _userManagerMock.Setup(m => m.GetRolesAsync(user))
                    .ReturnsAsync(new List<string> { "User" });

            // ACT
            var result = await _authService.CheckUserService(loginDto);

            // ASSERT
            Assert.True(result.success);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.data);
            Assert.False(string.IsNullOrEmpty(result.data.AccessToken));
            Assert.False(string.IsNullOrEmpty(result.data.RefreshToken));

        }
    }
}
