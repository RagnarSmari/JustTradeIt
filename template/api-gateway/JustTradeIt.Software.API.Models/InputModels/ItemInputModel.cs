using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JustTradeIt.Software.API.Models.CustomAttributes;
using JustTradeIt.Software.API.Models.Enums;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class ItemInputModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [ItemConditions]
        public string ConditionCode { get; set; }
        [Url]
        public IEnumerable<string> ItemImages { get; set; }
    }
}