using System.IO;
using System.Threading.Tasks;

namespace MeetAndGoMobile.Infrastructure.DependencyServices
{
    public interface IPicturePicker
    {
        Task<(Stream stream, string name, string format)> PickImageAsync();
    }
}
