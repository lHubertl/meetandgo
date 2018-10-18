using Microsoft.Extensions.Localization;

namespace MeetAndGoApi.Infrastructure.Resources
{
    public static class StringsExtension
    {
        public static string GetString(this IStringLocalizer<Strings> localizer, Strings key)
        {
            return localizer[key.ToString()];
        }
    }

    /// <summary>
    /// Do not forget to add translation to Strings.*.resx file, because this enum is only helper  
    /// </summary>
    public enum Strings
    {
        V_ParameterCanNotBeNull,
        V_CanNotFindData,
        V_LoginCredentialFail,
        V_PhoneNumber,
        V_Password,
        V_Code,
        V_DifferentPasswords,
        V_FirstName,
        V_LastName,
        V_UserExist,
        V_SignIn,
    }
}
