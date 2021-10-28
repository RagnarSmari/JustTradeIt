using Microsoft.AspNetCore.Http.Features;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class ProfileInputModel
    { 
        [MinLength(3)]
        public string fullName { get; set; }
        
        // An image file (See Uploading an image for reference)]
        [Display(Name="File")]
        public IFormFile profileImage { get; set; }
    }
}