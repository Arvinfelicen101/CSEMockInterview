using CSEMockInterview.DTOs.Auth;
using CSEMockInterview.Exceptions;
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

   

        // Assert + act
        await Assert.ThrowsAsync<BadRequestException>(() => service.CreateUserAsync(user));
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
        
        //assert + act
        var exception = await Record.ExceptionAsync(() => service.CreateUserAsync(user));
        Assert.Null(exception);

    }
    

}