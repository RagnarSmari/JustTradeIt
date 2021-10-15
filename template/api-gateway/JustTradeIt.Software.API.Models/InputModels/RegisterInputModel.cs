namespace JustTradeIt.Software.API.Models.InputModels
{
    public class RegisterInputModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        
        // Minimum of length 8
        // Must be equal to Password field within same class
        public string PasswordConfirmation { get; set; }
    }
}