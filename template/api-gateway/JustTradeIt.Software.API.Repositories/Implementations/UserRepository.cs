using System;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Interfaces;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public UserDto AuthenticateUser(LoginInputModel loginInputModel)
        {
            throw new NotImplementedException();
        }

        public UserDto CreateUser(RegisterInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        public UserDto GetProfileInformation(string email)
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserInformation(string userIdentifier)
        {
            throw new NotImplementedException();
        }

        public void UpdateProfile(string email, string profileImageUrl, ProfileInputModel profile)
        {
            throw new NotImplementedException();
        }
    }
}