using System.Linq;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Repositories.Data;
using JustTradeIt.Software.API.Repositories.Interfaces;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class TokenRepository : ITokenRepository
    {
        private readonly JustTradeItDbContext _db;

        public TokenRepository(JustTradeItDbContext db)
        {
            _db = db;
        }

        public JwtToken CreateNewToken()
        {
            var token = new JwtToken();
            _db.JwtTokens.Add(token);
            _db.SaveChanges();
            return token;
        }

        public bool IsTokenBlacklisted(int tokenId)
        {
            var token = _db.JwtTokens.FirstOrDefault(t => t.Id == tokenId);
            if (token == null) { return true; }
            return token.Blacklisted; 
        }

        public void VoidToken(int tokenId)
        {
            var token = _db.JwtTokens.FirstOrDefault(t => t.Id == tokenId);
            token.Blacklisted = true;
            _db.SaveChanges();
        }
    }
}