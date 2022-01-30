namespace JwtAuthentication.API.Models
{
    public class AuthRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(UserName)
                && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
