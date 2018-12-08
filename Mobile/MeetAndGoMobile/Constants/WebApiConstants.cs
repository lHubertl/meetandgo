﻿namespace MeetAndGoMobile.Constants
{
    internal static class WebApiConstants
    {
        internal static readonly string UrlScheme = "http";
        internal static readonly string ServerAddress = "localhost:5000/api";
        internal static readonly string Url = $"{UrlScheme}://{ServerAddress}";

        internal static readonly string AccountRegister = $"{Url}/account/Register";
        internal static readonly string AccountConfirmPhone = $"{Url}/account/ConfirmPhoneNumber";
        internal static readonly string AccountConfirmCode = $"{Url}/account/ConfirmMessageCode";
        internal static readonly string AccountSignIn = $"{Url}/account/SignIn";
        internal static readonly string AccountUserModel = $"{Url}/account/GetUserModel";

        internal static readonly string ContentTypeJson = "application/json";
        internal static readonly string Authorization = "Authorization";
    }
}
