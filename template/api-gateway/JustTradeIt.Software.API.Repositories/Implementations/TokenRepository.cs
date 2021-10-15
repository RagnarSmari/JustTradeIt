using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Repositories.Interfaces;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class TokenRepository : ITokenRepository
    {
        public JwtToken CreateNewToken()
        {
            throw new System.NotImplementedException();
        }

        public bool IsTokenBlacklisted(int tokenId)
        {
            throw new System.NotImplementedException();
        }

        public void VoidToken(int tokenId)
        {
            throw new System.NotImplementedException();
        }
    }
}