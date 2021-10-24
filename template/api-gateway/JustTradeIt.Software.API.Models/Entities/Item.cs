using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JustTradeIt.Software.API.Models.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string PublicIdentifier { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        
        // Both of these are foreign keys in this table
        public int ItemConditionId { get; set; }
        public bool IsDeleted { get; set; }
        public User Owner { get; set; }
        public ItemCondition ItemConditionLink {get; set;}
        public ICollection<TradeItem> TradeItems { get; set; }
        public ICollection<ItemImage> ItemImages {get; set;}
    }
}