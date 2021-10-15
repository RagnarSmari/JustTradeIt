using System.Collections;
using System.Collections.Generic;
using JustTradeIt.Software.API.Models.Dtos;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class TradeInputModel
    {
        public string ReceiverIdentifier { get; set; }
        
        // All items included in the trade request, for both receiver and initiator of trade
        public IEnumerable<ItemDto> ItemsInTrade { get; set; }
    }
}