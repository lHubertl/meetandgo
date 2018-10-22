using MeetAndGoMobile.Infrastructure.Resources;
using System;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeetAndGoMobile.Infrastructure.Extensions
{
    [ContentProperty("Text")]
    public class TranslationExtension : IMarkupExtension
    {
        private const string ResourceId = "MeetAndGoMobile.Infrastructure.Resources.Strings";
        public string Text { get; set; }

        private static readonly Lazy<ResourceManager> ResourceManager = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, typeof(TranslationExtension).GetTypeInfo().Assembly));

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return string.Empty;
            }

            var translation = ResourceManager.Value.GetString(Text, Strings.Culture) ?? Text;

            return translation;
        }
    }
}
