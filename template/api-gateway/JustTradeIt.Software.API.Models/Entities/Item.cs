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
        public ItemCondition ItemCondition { get; set; }
        public User OwnerId { get; set; }
        // Both of these are foreign keys in this table
        public ICollection<User> Users { get; set; }
        public ICollection<Trade> Trades { get; set; }
    }
}