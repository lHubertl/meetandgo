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
        ParameterCanNotBeNull,
        CanNotFindData,
        LoginCredentialFail,
        PhoneNumber,
        Password,
        Code,
        DifferentPasswords,
        FirstName,
        LastName,
        UserExist,
        SignIn,
        DirectionPointless,
        UserNotFound,
        NameCanNotBeEmpty,
    }
}
