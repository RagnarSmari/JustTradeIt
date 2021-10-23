using System;
using System.Text.Json.Serialization;
using JustTradeIt.Software.API.Models.Enums;

namespace JustTradeIt.Software.API.Models.Dtos
{
    public class UserDto
    {
        public string Identifier { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int TokenId { get; set; }
    }
}