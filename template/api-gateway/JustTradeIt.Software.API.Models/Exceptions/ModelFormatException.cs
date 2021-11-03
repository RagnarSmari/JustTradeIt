using System;

namespace JustTradeIt.Software.API.Models.Exceptions
{
    public class ModelFormatException : Exception
    {
        public ModelFormatException() : base("The model is not properly formatted.") {}
        
        public ModelFormatException(string message) : base(message) {}
        
        public ModelFormatException(string message, Exception inner) : base(message, inner) {}
    }
}