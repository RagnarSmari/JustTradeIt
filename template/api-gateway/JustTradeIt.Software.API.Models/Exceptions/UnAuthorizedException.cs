using System;

namespace JustTradeIt.Software.API.Models.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException() : base() {}
        
        public UnAuthorizedException(string message) : base(message) {}
        
        public UnAuthorizedException(string message, Exception inner) : base(message, inner) {}
    }
}