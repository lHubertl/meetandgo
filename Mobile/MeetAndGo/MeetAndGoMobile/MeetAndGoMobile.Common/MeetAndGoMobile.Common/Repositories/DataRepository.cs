using System.Threading.Tasks;
using MeetAndGoMobile.Common.Enums;

namespace MeetAndGoMobile.Common.Repositories
{
    public class DataRepository : IDataRepository
    {
        public DataRepository()
        {

        }

        public Task<T> GetAsync<T>(DataRepositoryKey dataType)
        {
            return null;
        }

        public Task UpdateAsync<T>(DataRepositoryKey dataType, T data)
        {
            return null;
        }

        public Task DeleteAsync<T>(DataRepositoryKey dataType)
        {
            return null;
        }
    }
}
