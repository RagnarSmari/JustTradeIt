using System;
using System.Collections;
using JustTradeIt.Software.API.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public int RecieverId { get; set; }
        public int SenderId { get; set; }
        
        public virtual User Reciever { get; set; }
        public virtual User Sender { get; set; }
        public ICollection<TradeItem> TradeItems { get; set; }
    }
}