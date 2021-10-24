using System;
using System.Linq;
using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Models.Entities;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Enums;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Data;
using JustTradeIt.Software.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
            var itemCondition = _db.ItemConditions.FirstOrDefault(c => c.ConditionCode == item.ConditionCode);
            if (itemCondition == null)
            {
                // Create the new item
                itemCondition = CreateNewItemCondition(item.ConditionCode);
            }
            var owner = _db.Users.FirstOrDefault(c => c.FullName == email);
            var newItem = new Item{
                PublicIdentifier = Guid.NewGuid().ToString(),
                Title = item.Title,
                Description = item.Description,
                ShortDescription = item.ShortDescription,
                ItemConditionId = itemCondition.Id,
                Owner = owner,
                IsDeleted = false,
            };
            _db.Items.Add(newItem);
            _db.SaveChanges();
            return newItem.PublicIdentifier;
        }

        private ItemCondition CreateNewItemCondition(string conditionCode)
        {
            var item = new ItemCondition
            {
                ConditionCode = conditionCode,
                Description = null,
            };
            _db.ItemConditions.Add(item);
            _db.SaveChanges();
            return item;

        }

        public Envelope<ItemDto> GetAllItems(int maxPages, int pageSize, int pageNumber, bool ascendingSortOrder)
        {
            var allItems = _db.Items
            .Include(c => c.Owner)
            .Where( c=> c.IsDeleted == false)
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
            var item = _db.Items
                .Include(c => c.ItemImages)
                .Include(c => c.Owner)
                .Include(c => c.TradeItems)
                .ThenInclude(c => c.Trade)
                .FirstOrDefault(c => c.PublicIdentifier == identifier);
            // TODO implement item not found exception
            // if (item.IsDeleted == true)
            // {
            //     throw new ItemnotFoundException();
            // }
            var itemId = item.Id;
            var activeTrades = item.TradeItems.Count(c => c.Trade.TradeStatus.ToString() == "Pending");

            var newItem = new ItemDetailsDto
            {
                Identifier = item.PublicIdentifier,
                Title = item.Title,
                Description = item.Description,
                Images = item.ItemImages.Select(c => new ImageDto{
                    Id = c.Id,
                    ImageUlr = c.ImageUrl
                }),
                NumberOfActiveTradeRequests = activeTrades,
                Condition = _db.ItemConditions.Where(s => s.Id == item.ItemConditionId)
                    .Select(c => c.ConditionCode).FirstOrDefault(),
                Owner = new UserDto()
                {
                    Identifier = item.Owner.PublicIdentifier,
                    FullName = item.Owner.FullName,
                    Email = item.Owner.Email,
                    ProfileImageUrl = item.Owner.ProfileImageUrl,
                }
            };
            return newItem;
        }

        public void RemoveItem(string email, string identifier)
        {
            // Get the item
            // Check if the user is the owner of that item
            // If so soft delete the item and mark all trades as cancelled 
            // Else just return
            var item = _db.Items
                .Include(c => c.Owner)
                .Include(c => c.TradeItems)
                .ThenInclude(c => c.Trade)
                .FirstOrDefault(c => c.PublicIdentifier == identifier);
            
            var user = _db.Users.FirstOrDefault(c => c.FullName == email);
            // TODO IMPLEMENT exception
            if (user.Id != item.Owner.Id)
            {
                throw new Exception();
            }
            
            // Soft delete the item
            item.IsDeleted = true;
            
            // Get all the trades and mark them as cancelled
            var tradeItems = item.TradeItems.Select(c => c.Trade);
            foreach (var tradeItem in tradeItems)
            {
                tradeItem.TradeStatus = TradeStatus.Cancelled;
            }
            _db.SaveChanges();

        }
    }
}