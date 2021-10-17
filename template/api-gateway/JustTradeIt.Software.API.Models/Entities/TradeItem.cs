namespace JustTradeIt.Software.API.Models.Entities
{
    public class TradeItem
    {
        public int TradeId { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        
        // NAv props
        public Trade Trade { get; set; }
        public User User { get; set; }
        public Item Item { get; set; }
    }
}