using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class ItemService : IItemService
    {
        public string AddNewItem(string email, ItemInputModel item)
        {
            throw new System.NotImplementedException();
        }

        public ItemDetailsDto GetItemByIdentifier(string identifier)
        {
            throw new System.NotImplementedException();
        }

        public Envelope<ItemDto> GetItems(int pageSize, int pageNumber, bool ascendingSortOrder)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveItem(string email, string itemIdentifier)
        {
            throw new System.NotImplementedException();
        }
    }
}