using System;
using System.Linq;
using System.Text;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.Exceptions;
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
            if (user == null)
            {
                throw new Exception("User not found or credentials are incorrect");
            }

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
            // Check if there is a user in the database with that email already
            if (_db.Users.FirstOrDefault(c => c.Email == inputModel.Email) != null)
            {
                throw new ResourceAlreadyExistsException("There is already a user with that email");
            }
            // Create a new JWT token
            var token = new JwtToken();
            // Create a new identifier for the new user
            var identifier = Guid.NewGuid().ToString();
            
            // Hash the password
            // Create the user which goes into the database, 
            var user = new User
            {
                PublicIdentifier = identifier,
                FullName = inputModel.FullName,
                Email = inputModel.Email,
                HashedPassword = HashPassword(inputModel.Password, _salt)
            };
            
            // Add the stuff to the database, save changes and return the userDto
            _db.Users.Add(user);
            _db.JwtTokens.Add(token);
            
            _db.SaveChanges();
            return new UserDto()
            {
                Identifier = user.PublicIdentifier,
                FullName = user.FullName,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
            };
        }

        public UserDto GetProfileInformation(string name)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == name);
            var token = new JwtToken();
            if (user == null)
            {
                throw new ResourceNotFoundException("User not found");
            }

            var newuser = new UserDto
            {
                Identifier = user.PublicIdentifier,
                FullName = user.FullName,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
            };
            return newuser;
        }

        public UserDto GetUserInformation(string userIdentifier)
        {
            var user = _db.Users.FirstOrDefault(u => u.PublicIdentifier == userIdentifier);
            if (user == null)
            {
                throw new ResourceNotFoundException("User not found");
            }

            return new UserDto
            {
                Identifier = user.PublicIdentifier,
                FullName = user.FullName,
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
            };
        }
        

        public void UpdateProfile(string email, string profileImageUrl, ProfileInputModel profile)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new ResourceNotFoundException("User not found");
            }
            // Change the name and the image
            user.FullName = profile.fullName;
            user.ProfileImageUrl = profileImageUrl;
            _db.SaveChanges();

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