using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class ProfileInputModel
    { 
        [MinLength(3)]
        public string FullName { get; set; }
        
        // An image file (See Uploading an image for reference)
        [Url]
        public IFormFile ProfileImage { get; set; }
    }
}