using System;
using System.Linq;
using System.Text;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Data;
using JustTradeIt.Software.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly JustTradeItDbContext _db;
        //private readonly TokenRepository _tokenRepository;
        private string _salt = "00209b47-08d7-475d-a0fb-20abf0872ba0";
        
        public UserRepository(JustTradeItDbContext db)
        {
            _db = db;
            //_tokenRepository = tokenRepository;
        }
        public UserDto AuthenticateUser(LoginInputModel login)
        {
            var user = _db.Users.FirstOrDefault(c =>
                c.Email == login.Email &&
                c.HashedPassword == HashPassword(login.Password, _salt));
            if (user == null){ return null; }

            var token = new JwtToken();

            _db.JwtTokens.Add(token);
            _db.SaveChanges();
            return new UserDto
            {
                Identifier = user.PublicIdentifier,
                FullName = user.FullName,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                TokenId = token.Id
            };
        }
        

        public UserDto CreateUser(RegisterInputModel inputModel)
        {
            // TODO remove this url and make it use the service or something
            var profileImageUrl = "https://www.realmenrealstyle.com/wp-content/uploads/2021/06/man-face.jpg";
            // Create a new JWT token for the user
            //var token = _tokenRepository.CreateNewToken();
            var token = new JwtToken();
            _db.JwtTokens.Add(token);
            // Create a new identifier for the new user
            var identifier = Guid.NewGuid().ToString();
            
            // Create the user which goes into the database, but we will return the dto
            var user = new User
            {
                PublicIdentifier = identifier,
                FullName = inputModel.FullName,
                Email = inputModel.Email,
                ProfileImageUrl = profileImageUrl,
                HashedPassword = HashPassword(inputModel.Password, _salt)
            };
            // Add the stuff to the database, save changes and return the userDto
            _db.Users.Add(user);
            
            _db.SaveChanges();
            return new UserDto()
            {
                Identifier = user.PublicIdentifier,
                FullName = user.FullName,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                TokenId = token.Id,
            };
        }

        public UserDto GetProfileInformation(string email)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == email);
            var token = new JwtToken();
            if (user == null) {return null;}

            var newuser = new UserDto
            {
                Identifier = user.PublicIdentifier,
                FullName = user.FullName,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                TokenId = token.Id
            };
            return newuser;
        }

        public UserDto GetUserInformation(string userIdentifier)
        {
            throw new NotImplementedException();
        }
        

        public void UpdateProfile(string email, string profileImageUrl, ProfileInputModel profile)
        {
            // TODO 
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