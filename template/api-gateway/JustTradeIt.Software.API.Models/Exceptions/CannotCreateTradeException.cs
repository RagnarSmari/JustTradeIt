using System;

namespace JustTradeIt.Software.API.Models.Exceptions
{
    public class CannotCreateTradeException : Exception
    {
     
        public CannotCreateTradeException() : base("Cannot update trade") {}
        
        public CannotCreateTradeException(string message) : base(message) {}
        
        public CannotCreateTradeException(string message, Exception inner) : base(message, inner) {}
        
    }
}