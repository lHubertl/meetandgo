using MeetAndGoApi.Infrastructure.Resources;
using Microsoft.Extensions.Localization;

namespace MeetAndGoApi.Infrastructure.Services
{
    public class BaseService
    {
        protected IStringLocalizer<Strings> Localizer { get; }

        public BaseService(IStringLocalizer<Strings> localizer)
        {
            Localizer = localizer;
        }
    }
}
