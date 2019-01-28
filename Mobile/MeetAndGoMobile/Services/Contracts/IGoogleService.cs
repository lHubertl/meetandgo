using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Maps;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;

namespace MeetAndGoMobile.Services.Contracts
{
    public interface IGoogleService
    {
        Task<IResponseData<List<PointModel>>> GetPointsByInputText(string text, LatLng location);
    }
}
