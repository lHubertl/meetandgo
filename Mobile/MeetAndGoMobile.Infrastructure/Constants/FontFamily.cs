using Xamarin.Forms;

namespace MeetAndGoMobile.Infrastructure.Constants
{
    public static class FontFamily
    {
        public static readonly string Default;

        public static readonly string NunitoLight;
        public static readonly string NunitoRegular;
        public static readonly string NunitoBold;

        static FontFamily()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    NunitoLight = "Nunito-Light";
                    NunitoRegular = "Nunito-Regular";
                    NunitoBold = "Nunito-Bold";
                    break;

                case Device.Android:
                    NunitoLight = "Nunito-Light.ttf#Nunito-Light";
                    NunitoRegular = "Nunito-Regular.ttf#Nunito-Regular";
                    NunitoBold = "Nunito-Bold.ttf#Nunito-Bold";
                    break;

                case Device.UWP:
                    NunitoLight = "Assets/Fonts/Nunito-Light.ttf#Nunito";
                    NunitoRegular = "Assets/Fonts/Nunito-Regular.ttf#Nunito";
                    NunitoBold = "Assets/Fonts/Nunito-Bold.ttf#Nunito";
                    break;
            }

            Default = NunitoRegular;
        }
    }
}
