namespace MatrimonyAPI.Models
{
    public class AuthenticateResponse
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? MobileNo { get; set; }
        public string? Token { get; set; }
    }
}
