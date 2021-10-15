using Microsoft.AspNetCore.Http;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class ProfileInputModel
    {
        public string FullName { get; set; }
        
        // An image file (See Uploading an image for reference)
        public IFormFile ProfileImage { get; set; }
    }
}