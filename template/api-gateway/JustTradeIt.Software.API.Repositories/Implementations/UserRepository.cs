using System;
using System.Text;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Data;
using JustTradeIt.Software.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public readonly JustTradeItDbContext _dbContext;
        private string _salt = "abc";

        private UserRepository(JustTradeItDbContext dbContext)
        {
            _dbContext = dbContext;
        }
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

        public UserDto Login(LoginInputModel login)
        {
            throw new NotImplementedException();
        }

        public void Logout(int tokenId)
        {
            throw new NotImplementedException();
        }

        public bool IsTokenBlacklisted(int tokenId)
        {
            throw new NotImplementedException();
        }

        public void UpdateProfile(string email, string profileImageUrl, ProfileInputModel profile)
        {
            throw new NotImplementedException();
        }

        public static string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: CreateSalt(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        private static byte[] CreateSalt(string salt) =>
            Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(salt)));
    }
}