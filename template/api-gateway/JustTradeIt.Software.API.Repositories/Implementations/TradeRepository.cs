using System;
using System.Collections.Generic;
using System.Linq;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.Enums;
using JustTradeIt.Software.API.Models.Exceptions;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Data;
using JustTradeIt.Software.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            var myUser = _db.Users.FirstOrDefault(c => c.Email == email);
            // Get the user which we want to trade with
            var otherUser = _db.Users.FirstOrDefault(c => c.PublicIdentifier == trade.ReceiverIdentifier);
            
            // Check if the users are null, if not continue
            if (myUser == null || otherUser == null)
            {
                throw new ResourceNotFoundException();
            }
            
            // If the user is trying to make a trade to himself -- does not make sense
            if (myUser.Id == otherUser.Id)
            {
                // TODO implement error
                throw new CannotCreateTradeException("User cannot make trade to himself");
            }


            var newTrade = new Trade
            {
                PublicIdentifier = Guid.NewGuid().ToString(),
                IssueDate = date,
                ModifiedDate = date,
                ModifiedBy = myUser.FullName,
                TradeStatus = status,
                RecieverId = otherUser.Id,
                SenderId = myUser.Id,
            };
            _db.Trades.Add(newTrade);
            
            // Get the items which are part of the trade and put them in a list of TradeItem
            ICollection<TradeItem> allItems = new List<TradeItem>();
            foreach (var item in trade.ItemsInTrade)
            {
                var thisItem = _db.Items.FirstOrDefault(c => c.PublicIdentifier == item.Identifier);
                if (thisItem == null)
                {
                    throw new ResourceNotFoundException("Item not found");
                }
                // If the owner is the authenticated user
                if (thisItem.Owner.Id == myUser.Id)
                {
                    var newItem = new TradeItem
                    {
                        TradeId = newTrade.Id,
                        ItemId = thisItem.Id,
                        UserId = myUser.Id,
                        Trade = newTrade,
                        User = myUser,
                        Item = thisItem
                    };
                    _db.TradeItems.Add(newItem);
                    allItems.Add(newItem);
                }
                // If the other user is the owner of the item
                else if (thisItem.Owner.Id == otherUser.Id)
                {
                    var newItem = new TradeItem
                    {
                        TradeId = newTrade.Id,
                        ItemId = thisItem.Id,
                        UserId = myUser.Id,
                        Trade = newTrade,
                        User = otherUser,
                        Item = thisItem
                    };
                    _db.TradeItems.Add(newItem);
                    allItems.Add(newItem);
                }
                // If the neither of the users that are part of the trade are owner of the item
                else
                {
                    throw new CannotCreateTradeException("An item in the trade is not linked to an participant of the trade request");
                }
            }

            newTrade.TradeItems = allItems;
            _db.SaveChanges();
            return newTrade.PublicIdentifier;

        }

        public TradeDetailsDto GetTradeByIdentifier(string identifier)
        {
            // Get the trade
            var trade = _db.Trades
                .Include(c => c.TradeItems)
                .ThenInclude(c => c.Item)
                .FirstOrDefault(c => c.PublicIdentifier == identifier);
            if (trade == null)
            {
                throw new ResourceNotFoundException();
            }
            
            // Issued date can be null as it only has value when the trade has been accepted
            Nullable<DateTime> time = null;
            if (trade.TradeStatus == TradeStatus.Accepted)
            {
                time = trade.ModifiedDate;
            }
            
            // Get both users
            var recUser = _db.Users
                .Include(c => c.TradeItems)
                .ThenInclude(c => c.Item)
                .FirstOrDefault(c => c.Id == trade.RecieverId);
            
            var sendUser = _db.Users
                .Include(c => c.TradeItems)
                .ThenInclude(c => c.Item)
                .FirstOrDefault(c => c.Id == trade.SenderId);
            
            // Get the items of the users that are associated with this trade

            var myRecItems = _db.TradeItems
                .Include(c => c.Item)
                .Where(c => c.TradeId == trade.Id)
                .Where(c => c.UserId == recUser.Id)
                .Select(c => new ItemDto
                {
                    Identifier = c.Item.PublicIdentifier,
                    Title = c.Item.Title,
                    ShortDescription = c.Item.ShortDescription,
                    Owner = new UserDto()
                    {
                        Identifier = c.Item.Owner.PublicIdentifier,
                        FullName = c.Item.Owner.FullName,
                        Email = c.Item.Owner.Email,
                        ProfileImageUrl = c.Item.Owner.ProfileImageUrl
                    }
                });
            var mySenderItems = _db.TradeItems
                .Include(c => c.Item)
                .Where(c => c.TradeId == trade.Id)
                .Where(c => c.UserId == sendUser.Id)
                .Select(c => new ItemDto
                {
                    Identifier = c.Item.PublicIdentifier,
                    Title = c.Item.Title,
                    ShortDescription = c.Item.ShortDescription,
                    Owner = new UserDto()
                    {
                        Identifier = c.Item.Owner.PublicIdentifier,
                        FullName = c.Item.Owner.FullName,
                        Email = c.Item.Owner.Email,
                        ProfileImageUrl = c.Item.Owner.ProfileImageUrl
                    }
                });

            // return a tradedetail
            return new TradeDetailsDto()
            {
                Identifier = trade.PublicIdentifier,
                ReceivingItems = myRecItems,
                OfferingItems = mySenderItems,
                Receiver = new UserDto
                {
                    Identifier = recUser.PublicIdentifier,
                    FullName = recUser.FullName,
                    Email = recUser.Email,
                    ProfileImageUrl = recUser.ProfileImageUrl
                },
                Sender = new UserDto
                {
                    Identifier = sendUser.PublicIdentifier,
                    FullName = sendUser.FullName,
                    Email = sendUser.Email,
                    ProfileImageUrl = sendUser.ProfileImageUrl
                },
                ReceivedDate = time,
                IssuedDate = trade.IssueDate,
                ModifiedDate = trade.ModifiedDate,
                ModifiedBy = trade.ModifiedBy,
                Status = trade.TradeStatus.ToString(),

            };
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive)
        {
            var user = _db.Users.FirstOrDefault(c => c.Email == email);
                
            if (user == null)
            {
                throw new ResourceNotFoundException("Couldn't find user");
            }

            var recTrades = _db.Trades
                .Where(c => c.RecieverId == user.Id)
                .Where(c => c.TradeStatus != TradeStatus.Accepted)
                .Select(c => new TradeDto
                {
                    Identifier = c.PublicIdentifier,
                    IssuedDate = c.IssueDate,
                    ModifiedDate = c.ModifiedDate,
                    ModifiedBy = c.ModifiedBy,
                    Status = c.TradeStatus.ToString()
                }).AsEnumerable();
            var sendTrades = _db.Trades
                .Where(c => c.SenderId == user.Id)
                .Where(c => c.TradeStatus != TradeStatus.Accepted)
                .Select(c => new TradeDto
                {
                    Identifier = c.PublicIdentifier,
                    IssuedDate = c.IssueDate,
                    ModifiedDate = c.ModifiedDate,
                    ModifiedBy = c.ModifiedBy,
                    Status = c.TradeStatus.ToString()
                });
            var allTrades = recTrades.Concat(sendTrades);
            return allTrades;
        }

        public IEnumerable<TradeDto> GetTrades(string email)
        {
            var user = _db.Users.FirstOrDefault(c => c.Email == email);
                
            if (user == null)
            {
                throw new ResourceNotFoundException("User not found");
            }

            var recTrades = _db.Trades
                .Where(c => c.RecieverId == user.Id)
                .Where(c => c.TradeStatus == TradeStatus.Accepted)
                .Select(c => new TradeDto
                {
                    Identifier = c.PublicIdentifier,
                    IssuedDate = c.IssueDate,
                    ModifiedDate = c.ModifiedDate,
                    ModifiedBy = c.ModifiedBy,
                    Status = c.TradeStatus.ToString()
                }).AsEnumerable();
            Console.WriteLine(recTrades);
            var sendTrades = _db.Trades
                .Where(c => c.SenderId == user.Id)
                .Where(c => c.TradeStatus == TradeStatus.Accepted)
                .Select(c => new TradeDto
                {
                    Identifier = c.PublicIdentifier,
                    IssuedDate = c.IssueDate,
                    ModifiedDate = c.ModifiedDate,
                    ModifiedBy = c.ModifiedBy,
                    Status = c.TradeStatus.ToString()
                });
            var allTrades = recTrades.Concat(sendTrades);
            return allTrades;
        }

        public IEnumerable<TradeDto> GetUserTrades(string userIdentifier)
        {
            var user = _db.Users.FirstOrDefault(c => c.PublicIdentifier == userIdentifier);
            Console.WriteLine(user.FullName);
            if (user == null)
            {
                throw new ResourceNotFoundException("User not found");
            }

            var recTrades = _db.Trades
                .Where(c => c.RecieverId == user.Id)
                .Where(c => c.TradeStatus == TradeStatus.Accepted)
                .Select(c => new TradeDto
                {
                    Identifier = c.PublicIdentifier,
                    IssuedDate = c.IssueDate,
                    ModifiedDate = c.ModifiedDate,
                    ModifiedBy = c.ModifiedBy,
                    Status = c.TradeStatus.ToString()
                }).AsEnumerable();
            var sendTrades = _db.Trades
                .Where(c => c.SenderId == user.Id)
                .Select(c => new TradeDto
                {
                    Identifier = c.PublicIdentifier,
                    IssuedDate = c.IssueDate,
                    ModifiedDate = c.ModifiedDate,
                    ModifiedBy = c.ModifiedBy,
                    Status = c.TradeStatus.ToString()
                });
            var allTrades = recTrades.Concat(sendTrades);
            return allTrades;
        }

        public TradeDetailsDto UpdateTradeRequest(string email, string identifier, TradeStatus newStatus)
        {
            // Updates the Tradestatus of the trade
            
            var user = _db.Users.FirstOrDefault(c => c.Email == email);
            var trade = _db.Trades.FirstOrDefault(c => c.PublicIdentifier == identifier);
            
            if (trade == null)
            {
                throw new ResourceNotFoundException("Trade not found");
            }
            
            var recUser = _db.Users
                .Include(c => c.TradeItems)
                .ThenInclude(c => c.Item)
                .FirstOrDefault(c => c.Id == trade.RecieverId);
            
            var sendUser = _db.Users
                .Include(c => c.TradeItems)
                .ThenInclude(c => c.Item)
                .FirstOrDefault(c => c.Id == trade.SenderId);
            if (recUser == null || sendUser == null)
            {
                throw new ResourceNotFoundException("Either the receiver was not found or the sender");
            }
            
            // Check if the tradestatus of the trade is active
            if (trade.TradeStatus != TradeStatus.Pending)
            {
                throw new CannotUpdateTradeException("Status of the trade is not pending");
            }
            if (user == null)
            {
                throw new ResourceNotFoundException("User not Found");
            }
            
            // Check if the new tradestatus is either Pending og TimedOut
            if (newStatus == TradeStatus.Pending || newStatus == TradeStatus.TimedOut)
            {
                throw new CannotUpdateTradeException("New status of the trade is not valid");
            }
            
            // If the user is the recUser of the trade then he can either accept or decline
            // If the user is the sendUser of the trade then he can only Accept the trade
            if (user.Id == sendUser.Id)
            {

                if (newStatus != TradeStatus.Cancelled)
                {
                    throw new CannotUpdateTradeException("This user can only cancel the trade request as he is the Sender");
                }
            }
            else
            {
                if (newStatus != TradeStatus.Accepted)
                {
                    if (newStatus != TradeStatus.Declined)
                    {
                        throw new CannotUpdateTradeException("This user can only Accept or Decline the trade request");   
                    }
                }
            }
            
            Nullable<DateTime> time = null;
            if (newStatus == TradeStatus.Accepted)
            {
                time = DateTime.Now;
            }
            
            trade.TradeStatus = newStatus;
            trade.ModifiedBy = user.FullName;
            trade.ModifiedDate = DateTime.Now;
            

            // Get the items of the users that are associated with this trade

            var myRecItems = _db.TradeItems
                .Include(c => c.Item)
                .Where(c => c.TradeId == trade.Id)
                .Where(c => c.UserId == recUser.Id)
                .Select(c => new ItemDto
                {
                    Identifier = c.Item.PublicIdentifier,
                    Title = c.Item.Title,
                    ShortDescription = c.Item.ShortDescription,
                    Owner = new UserDto()
                    {
                        Identifier = c.Item.Owner.PublicIdentifier,
                        FullName = c.Item.Owner.FullName,
                        Email = c.Item.Owner.Email,
                        ProfileImageUrl = c.Item.Owner.ProfileImageUrl
                    }
                });
            var mySenderItems = _db.TradeItems
                .Include(c => c.Item)
                .Where(c => c.TradeId == trade.Id)
                .Where(c => c.UserId == sendUser.Id)
                .Select(c => new ItemDto
                {
                    Identifier = c.Item.PublicIdentifier,
                    Title = c.Item.Title,
                    ShortDescription = c.Item.ShortDescription,
                    Owner = new UserDto()
                    {
                        Identifier = c.Item.Owner.PublicIdentifier,
                        FullName = c.Item.Owner.FullName,
                        Email = c.Item.Owner.Email,
                        ProfileImageUrl = c.Item.Owner.ProfileImageUrl
                    }
                });
            _db.SaveChanges();
            return new TradeDetailsDto()
            {
                Identifier = trade.PublicIdentifier,
                ReceivingItems = myRecItems,
                OfferingItems = mySenderItems,
                Receiver = new UserDto
                {
                    Identifier = recUser.PublicIdentifier,
                    FullName = recUser.FullName,
                    Email = recUser.Email,
                    ProfileImageUrl = recUser.ProfileImageUrl
                },
                Sender = new UserDto
                {
                    Identifier = sendUser.PublicIdentifier,
                    FullName = sendUser.FullName,
                    Email = sendUser.Email,
                    ProfileImageUrl = sendUser.ProfileImageUrl
                },
                ReceivedDate = time,
                IssuedDate = trade.IssueDate,
                ModifiedDate = trade.ModifiedDate,
                ModifiedBy = email,
                Status = trade.TradeStatus.ToString(),

            };
            
        }
    }
}