using System.Security.Claims;

namespace MeetAndGoApi.Extensions
{
    public static class UserExtension
    {
        public static string CurrentUserId(this ClaimsPrincipal user)
        {
            var userClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            return userClaim?.Value;
        }
    }
}
