using System;
using System.Collections.Generic;
using System.Linq;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.Enums;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Data;
using JustTradeIt.Software.API.Repositories.Interfaces;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class TradeRepository : ITradeRepository
    {
        private readonly JustTradeItDbContext _db;

        public TradeRepository(JustTradeItDbContext db)
        {
            _db = db;
        }

        public string CreateTradeRequest(string email, TradeInputModel trade)
        {
            
            var date = DateTime.Now;
            TradeStatus status = TradeStatus.Pending;
            
            // Get the user which is making the request
            var myUser = _db.Users.FirstOrDefault(c => c.FullName == email);
            // Get the user which we want to trade with
            var otherUser = _db.Users.FirstOrDefault(c => c.PublicIdentifier == trade.ReceiverIdentifier);

            if (myUser == null || otherUser == null)
            {
                // TODO implement not found exception
                throw new Exception();
            }

            // Get the items which are part of the trade
            // Check if the users are owners of the Items
            // If not throw an error
            ItemDto itemOne = null;
            ItemDto itemTwo = null;
            foreach (var item in trade.ItemsInTrade)
            {
                if (item.Owner.Identifier == myUser.PublicIdentifier)
                {
                    itemOne = item;
                }

                if (item.Owner.Identifier == myUser.PublicIdentifier)
                {
                    itemTwo = item;
                }
            }

            if (itemOne == null || itemTwo == null)
            {
                // TODO implement owners not found
                throw new Exception();
            }

            var newTrade = new Trade
            {
                PublicIdentifier = Guid.NewGuid().ToString(),
                IssueDate = date,
                ModifiedDate = date,
                ModifiedBy = myUser.FullName,
                TradeStatus = status,
                RecieverId = otherUser.Id,
                SenderId = myUser.Id
            };
            _db.Trades.Add(newTrade);
            _db.SaveChanges();
            return newTrade.PublicIdentifier;

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