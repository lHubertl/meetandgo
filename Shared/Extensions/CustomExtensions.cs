using System.Linq;

namespace MeetAndGo.Shared.Extensions
{
    public static class CustomExtensions
    {
        public static string CleanPhoneNumber(this string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return phoneNumber;
            }

            return new string(phoneNumber.Where(x => char.IsDigit(x)).ToArray());
        }
    }
}
