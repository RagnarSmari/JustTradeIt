using System.Threading.Tasks;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class AccountService : IAccountService
    {
        public bool IsTokenBlacklisted(int tokenId)
        {
            throw new System.NotImplementedException();
        }
        public UserDto AuthenticateUser(LoginInputModel loginInputModel)
        {
            throw new System.NotImplementedException();
        }

        public UserDto CreateUser(RegisterInputModel inputModel)
        {
            throw new System.NotImplementedException();
        }

        public UserDto GetProfileInformation(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Logout(int tokenId)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateProfile(string email, ProfileInputModel profile)
        {
            throw new System.NotImplementedException();
        }
    }
}