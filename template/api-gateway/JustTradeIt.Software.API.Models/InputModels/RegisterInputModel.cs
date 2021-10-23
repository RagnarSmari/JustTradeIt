using System.ComponentModel.DataAnnotations;

namespace JustTradeIt.Software.API.Models.InputModels
{
    public class RegisterInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(3)]
        public string FullName { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        
        // Minimum of length 8
        // Must be equal to Password field within same class
        [Required]
        [MinLength(8)]
        [Compare((nameof(Password)))]
        public string PasswordConfirmation { get; set; }
    }
}