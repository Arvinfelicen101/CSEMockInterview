using CSEMockInterview.DTOs;
using CSEMockInterview.DTOs.Auth;
using CSEMockInterview.Exceptions;
using CSEMockInterview.Models;
using CSEMockInterview.Repository.UserManagement;

namespace CSEMockInterview.Services.UserManagement
{
    public class UserManagementServices : IUserManagementServices
    {
        private readonly IUserManagementRepository _repository;
        
        public UserManagementServices(IUserManagementRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateUserAsync(RegisterDTO user)
        {
            if (user.password != user.confirmPassword)
                throw new BadRequestException("Password mismatched");

            var userInfo = new Users
            {
                UserName = user.username,
                Email = user.email,
                FirstName = user.firstName,
                MiddleName = user.middleName,
                LastName = user.lastName
            };

            await _repository.CreateUserAsync(userInfo, user.password);
        }


    }
}

