namespace MeetAndGo.Shared.Models.Authorization
{
    public class LoginModel
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
