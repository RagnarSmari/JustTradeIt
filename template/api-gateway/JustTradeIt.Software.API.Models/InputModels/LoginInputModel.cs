namespace JustTradeIt.Software.API.Models.InputModels
{
    public class LoginInputModel
    {
        // Must be a valid email
        public string Email { get; set; }
        
        // Minimum of length 8
        public string Password { get; set; }
    }
}