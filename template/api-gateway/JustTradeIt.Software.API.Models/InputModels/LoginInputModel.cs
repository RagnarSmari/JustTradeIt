using System.ComponentModel.DataAnnotations;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class LoginInputModel
    {
        // Must be a valid email
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        // Minimum of length 8
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}