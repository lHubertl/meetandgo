using System;

namespace MeetAndGoApi.Infrastructure.Extensions
{
    public static class GeolocationHelper
    {
        public static double GetDistance(double xLong, double xLat, double yLong, double yLat)
        {
            return Math.Sqrt(Math.Pow(yLat - xLat, 2) + Math.Pow(yLong - xLong, 2));
        }
    }
}
