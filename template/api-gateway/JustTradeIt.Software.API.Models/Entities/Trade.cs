using System;
using JustTradeIt.Software.API.Models.Enums;

namespace JustTradeIt.Software.API.Models.Entities
{
    public class Trade
    {
        public int Id { get; set; }
        public string PublicIdentifier { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        
        // Trying to use the enum here but not sure
        public TradeStatus TradeStatus { get; set; }
        
        // Foreign keys
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
    }
}