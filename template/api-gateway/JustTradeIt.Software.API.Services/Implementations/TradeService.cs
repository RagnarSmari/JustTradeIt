using System;
using System.Collections.Generic;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class TradeService : ITradeService
    {
        public string CreateTradeRequest(string email, TradeInputModel tradeRequest)
        {
            throw new NotImplementedException();
        }

        public TradeDetailsDto GetTradeByIdentifer(string tradeIdentifier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TradeDto> GetTrades(string email)
        {
            throw new NotImplementedException();
        }

        public void UpdateTradeRequest(string identifier, string email, string status)
        {
            throw new NotImplementedException();
        }
    }
}