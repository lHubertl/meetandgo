using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace MeetAndGoApi.Extensions
{
    public static class UserExtension
    {
        public static string CurrentUserId(this ClaimsPrincipal user)
        {
            var userClaim = user.Claims.FirstOrDefault(x => x.ValueType == JwtRegisteredClaimNames.Sub);
            return userClaim?.Value;
        }
    }
}
