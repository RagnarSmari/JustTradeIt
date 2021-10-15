using System;
using System.Collections.Generic;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Interfaces;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class TradeRepository : ITradeRepository
    {
        public string CreateTradeRequest(string email, TradeInputModel trade)
        {
            throw new NotImplementedException();
        }

        public TradeDetailsDto GetTradeByIdentifier(string identifier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TradeDto> GetTrades(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TradeDto> GetUserTrades(string userIdentifier)
        {
            throw new NotImplementedException();
        }

        public TradeDetailsDto UpdateTradeRequest(string email, string identifier, Models.Enums.TradeStatus newStatus)
        {
            throw new NotImplementedException();
        }
    }
}