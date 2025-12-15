using CSEMockInterview.DTOs.Auth;
using CSEMockInterview.Models;
using CSEMockInterview.Repository.UserManagement;
using CSEMockInterview.Services.UserManagement;
using Moq;


namespace CSEMockInterview.Tests.Services.UserManagement;

public class UserManagementServicesTest
{

    [Fact]
    public async Task CreateUserService_PasswordMismatch_Returns404()
    {
        // Arrange
        var repoMock = new Mock<IUserManagementRepository>();

        var service = new UserManagementServices(repoMock.Object);

        var user = new RegisterDTO()
        {
            username = "francis123",
            firstName = "Francis",
            middleName = "Pajarit",
            lastName = "Flores",
            password = "Francis123!",
            confirmPassword = "Francis123?"
        };

        // Act
        var result = await service.CreateUserService(user);

        // Assert
        Assert.False(result.success);
        Assert.NotSame(user.password, user.confirmPassword);

        repoMock.Verify(r => r.CreateUserAsync(It.IsAny<Users>(), It.IsAny<string>()), Times.Never);
    }

    public async Task CreateUserService_UserCreated_Success()
    {
        //arrange
        var repoMock = new Mock<IUserManagementRepository>();
        var service = new UserManagementServices(repoMock.Object);
        
        var user = new RegisterDTO()
        {
            username = "francis123",
            firstName = "Francis",
            middleName = "Pajarit",
            lastName = "Flores",
            password = "Francis123!",
            confirmPassword = "Francis123!"
        };
        
        //act 
        var result = await service.CreateUserService(user);
        
        //assert
        Assert.Equal(200, result.StatusCode);

    }
    

}