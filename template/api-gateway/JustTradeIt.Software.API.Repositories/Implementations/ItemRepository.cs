using System;
using System.Linq;
using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Data;
using JustTradeIt.Software.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly JustTradeItDbContext _db;

        public ItemRepository(JustTradeItDbContext db){
            _db = db;
        }
        
        public string AddNewItem(string email, ItemInputModel item)
        {
            // TODO put ItemImages in the Item which goes into the database
            var owner = _db.Users.FirstOrDefault(c => c.Email == email);
            var newItem = new Item{
                PublicIdentifier = Guid.NewGuid().ToString(),
                Title = item.Title,
                Description = item.Description,
                ShortDescription = item.ShortDescription,
                ItemCondition = item.ConditionCode,
            };
            _db.Items.Add(newItem);
            _db.SaveChanges();
            return newItem.PublicIdentifier;
        }

        public Envelope<ItemDto> GetAllItems(int maxPages, int pageSize, int pageNumber, bool ascendingSortOrder)
        {
            var allItems = _db.Items
            .Include(c => c.Owner)
            .Select(c => new ItemDto{
                Identifier = c.PublicIdentifier,
                Title = c.Title,
                ShortDescription = c.ShortDescription,
                Owner = new UserDto{
                    Identifier = c.Owner.PublicIdentifier,
                    FullName = c.Owner.FullName,
                    Email = c.Owner.Email,
                    ProfileImageUrl = c.Owner.ProfileImageUrl,
                }
            });
            return new Envelope<ItemDto>{
                PageSize = pageSize,
                PageNumber = pageNumber,
                MaxPages = maxPages,
                Items = allItems
            }; 
        }

        public ItemDetailsDto GetItemByIdentifier(string identifier)
        {
            var itemId = _db.Items.FirstOrDefault(c => c.PublicIdentifier == identifier).Id;
            var activeTrades = _db.TradeItems.Where( c => c.ItemId == itemId).Count(c => c.Trade.TradeStatus.ToString() == "Pending");
            var item = _db.Items
            .Include(c => c.ItemImages)
            .FirstOrDefault(c => c.PublicIdentifier == identifier);
            return new ItemDetailsDto{
                Identifier = item.PublicIdentifier,
                Title = item.Title,
                Description = item.Description,
                Images = item.ItemImages.Select(c => new ImageDto{
                    Id = c.Id,
                    ImageUlr = c.ImageUrl
                }),
                NumberOfActiveTradeRequests = activeTrades,
                Condition = item.ItemCondition.ToString(),
                Owner = new UserDto{
                    Identifier = item.Owner.PublicIdentifier,
                    FullName = item.Owner.FullName,
                    Email = item.Owner.Email,
                    ProfileImageUrl = item.Owner.ProfileImageUrl
                }
            };
        }

        public void RemoveItem(string email, string identifier)
        {
            throw new NotImplementedException();
        }
    }
}