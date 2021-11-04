using System;

namespace JustTradeIt.Software.API.Models.Exceptions
{
    public class CannotDeleteItemException : Exception
    {
        public CannotDeleteItemException() : base("Cannot update trade") {}
        
        public CannotDeleteItemException(string message) : base(message) {}
        
        public CannotDeleteItemException(string message, Exception inner) : base(message, inner) {}
    }
}