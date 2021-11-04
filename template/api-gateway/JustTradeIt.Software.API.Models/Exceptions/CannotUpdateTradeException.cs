using System;

namespace JustTradeIt.Software.API.Models.Exceptions
{
    public class CannotUpdateTradeException : Exception
    {
        public CannotUpdateTradeException() : base("Cannot update trade") {}
        
        public CannotUpdateTradeException(string message) : base(message) {}
        
        public CannotUpdateTradeException(string message, Exception inner) : base(message, inner) {}
    }
}