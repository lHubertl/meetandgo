namespace MeetAndGoMobile.Infrastructure.Constants
{
    public static class FontFamily
    {
        public static readonly string Default;

        static FontFamily()
        {
            Default = Xamarin.Forms.Font.Default.FontFamily;
        }
    }
}
