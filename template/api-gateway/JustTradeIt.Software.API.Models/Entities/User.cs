namespace JustTradeIt.Software.API.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string PublicIdentifier { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; }
        public string HashedPassword { get; set; }
    }
}