using System;
using System.Collections.Generic;
using System.Linq;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Enums;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Interfaces;
using JustTradeIt.Software.API.Services.Interfaces;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly IQueueService _queueService;
        
        public TradeService(ITradeRepository tradeRepository, IQueueService queueService)
        {
            _tradeRepository = tradeRepository;
            _queueService = queueService;
        }

        public string CreateTradeRequest(string email, TradeInputModel tradeRequest)
        {
            var id =  _tradeRepository.CreateTradeRequest(email, tradeRequest);
            var trade = _tradeRepository.GetTradeByIdentifier(id);
            _queueService.PublishMessage("new-trade-request", trade.Receiver.Email);
             return id;
        }

        public TradeDetailsDto GetTradeByIdentifer(string tradeIdentifier)
        {
            return _tradeRepository.GetTradeByIdentifier(tradeIdentifier);
        }

        public IEnumerable<TradeDto> GetTradeRequests(string email, bool onlyIncludeActive = true)
        {
            return _tradeRepository.GetTradeRequests(email, onlyIncludeActive);
        }

        public IEnumerable<TradeDto> GetTrades(string email)
        {
            return _tradeRepository.GetTrades(email);
        }

        public void UpdateTradeRequest(string identifier, string email, string status)
        {
            // Change the status to enum type
            var myList = Enum.GetValues(typeof(TradeStatus))
                .Cast<TradeStatus>()
                .Select(v => v.ToString())
                .ToList();
            TradeStatus tradeStatus;
            try
            {
                tradeStatus = (TradeStatus)Enum.Parse(typeof(TradeStatus), status);
            }
            catch (Exception)
            {
                var errorString = "";
                for (int i = 0; i < myList.Count; i++)
                {
                    errorString += myList[i] + ", ";
                }

                throw new Exception("new status must be one of " + errorString);
            }
            var myTrade = _tradeRepository.UpdateTradeRequest(email, identifier, tradeStatus);
            _queueService.PublishMessage("trade-update-request",myTrade);
            
        }
    }
}