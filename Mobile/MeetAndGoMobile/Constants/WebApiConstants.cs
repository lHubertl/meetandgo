namespace MeetAndGoMobile.Constants
{
    internal static class WebApiConstants
    {
        internal static readonly string UrlScheme = "http";
        internal static readonly string ServerAddress = "192.168.88.165:5000/api";
        internal static readonly string Url = $"{UrlScheme}://{ServerAddress}";

        internal static readonly string GoogleApiKey = "AIzaSyBH3CK20ri1RtDo9c4BUToJ0xdVtH84YBE";

        internal static readonly string AccountRegister = $"{Url}/account/Register";
        internal static readonly string AccountConfirmPhone = $"{Url}/account/ConfirmPhoneNumber";
        internal static readonly string AccountConfirmCode = $"{Url}/account/ConfirmMessageCode";
        internal static readonly string AccountSignIn = $"{Url}/account/SignIn";
        internal static readonly string AccountUserModel = $"{Url}/account/GetUserModel";
        internal static readonly string AccountUploadProfilePhoto = $"{Url}/account/UploadProfilePhoto";
        internal static readonly string AccountUpdateUserModel = $"{Url}/account/UpdateUserModel";

        internal static readonly string EventHistory = $"{Url}/event/geteventshistory";

        internal static readonly string ContentTypeJson = "application/json";
        internal static readonly string Authorization = "Authorization";
    }
}
