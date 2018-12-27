using System.IO;
using System.Threading.Tasks;

namespace MeetAndGoMobile.Infrastructure.DependencyServices
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}
