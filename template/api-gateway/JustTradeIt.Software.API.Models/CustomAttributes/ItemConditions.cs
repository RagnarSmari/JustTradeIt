using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace JustTradeIt.Software.API.Models.CustomAttributes
{
    public class ItemConditions : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var exp = value.ToString();
            string[] correctValues = {"MINT", "GOOD", "USED", "BAD","DAMAGED"};
            if (correctValues.Contains(exp))
            {
                return ValidationResult.Success;
            }
            
            var errorString = "";
            for (int i = 0; i < correctValues.Length; i++)
            {
                errorString += correctValues[i] + ", ";
            }
            return new ValidationResult("Attempted value: "+$"{value}. " + "Expertize value must be: " +  errorString);
        }
    }
}