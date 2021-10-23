using JustTradeIt.Software.API.Models;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Services.Interfaces;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepo;
        public ItemService(IItemRepository itemRepository){
            _itemRepo = itemRepository;
        }
        public string AddNewItem(string email, ItemInputModel item)
        {
            return _itemRepo.AddNewItem(email, item);
        }

        public ItemDetailsDto GetItemByIdentifier(string identifier)
        {
            return _itemRepo.GetItemByIdentifier(identifier);
        }

        public Envelope<ItemDto> GetItems(int maxPages,int pageSize, int pageNumber, bool ascendingSortOrder)
        {
            return _itemRepo.GetAllItems(maxPages ,pageSize, pageNumber, ascendingSortOrder);
        }

        public void RemoveItem(string email, string itemIdentifier)
        {
            throw new System.NotImplementedException();
        }
    }
}