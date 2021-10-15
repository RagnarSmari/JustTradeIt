using System.Collections;
using System.Collections.Generic;
using JustTradeIt.Software.API.Models.Enums;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class ItemInputModel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public TradeStatus ConditionCode { get; set; }
        public IEnumerable<string> ItemImages { get; set; }
    }
}