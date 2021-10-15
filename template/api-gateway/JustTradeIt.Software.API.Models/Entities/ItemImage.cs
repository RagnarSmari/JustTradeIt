namespace JustTradeIt.Software.API.Models.Entities
{
    public class ItemImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        
        // Foreign key to Itemtable
        public int ItemId { get; set; }
    }
}