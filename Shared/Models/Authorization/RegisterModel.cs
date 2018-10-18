namespace MeetAndGo.Shared.Models.Authorization
{
    public class RegisterModel : LoginModel
    {
        public string Name { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
