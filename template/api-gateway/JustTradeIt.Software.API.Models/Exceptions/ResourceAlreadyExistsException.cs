using System;

namespace JustTradeIt.Software.API.Models.Exceptions
{
    public class ResourceAlreadyExistsException : Exception
    {
        public ResourceAlreadyExistsException() : base("Cannot update trade") {}
        
        public ResourceAlreadyExistsException(string message) : base(message) {}
        
        public ResourceAlreadyExistsException(string message, Exception inner) : base(message, inner) {}
    }
}