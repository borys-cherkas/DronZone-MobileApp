namespace DronZone_UWP.Models.Auth
{
    public class LoginModel
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; } = false;
    }
}