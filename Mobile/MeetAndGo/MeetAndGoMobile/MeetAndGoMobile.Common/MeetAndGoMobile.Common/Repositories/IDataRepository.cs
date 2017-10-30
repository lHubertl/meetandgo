using System.Threading.Tasks;
using MeetAndGoMobile.Common.Enums;

namespace MeetAndGoMobile.Common.Repositories
{
    /// <summary>
    /// This data repository will get/update/delete data localy 
    /// </summary>
    public interface IDataRepository
    {
        Task<T> GetAsync<T>(DataRepositoryKey dataType);

        Task UpdateAsync<T>(DataRepositoryKey dataType, T data);

        Task DeleteAsync<T>(DataRepositoryKey dataType);

    }
}
