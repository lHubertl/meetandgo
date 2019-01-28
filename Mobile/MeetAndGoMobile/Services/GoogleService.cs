using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Maps;
using Google.Maps.Places;
using MeetAndGo.Shared.BusinessLogic.Responses;
using MeetAndGo.Shared.Models;
using MeetAndGoMobile.Constants;
using MeetAndGoMobile.Infrastructure.BusinessLogic;
using MeetAndGoMobile.Services.Contracts;

namespace MeetAndGoMobile.Services
{
    public class GoogleService : HttpBaseService, IGoogleService
    {

        //    public async Task<IResponseData<object>> AutocompleteAsync(string input)
        //    {
        //        if (string.IsNullOrEmpty(input))
        //        {
        //            return new ResponseData<object>();
        //        }


        //        try
        //        {
        //            //var result = Client.Places.TextSearch(input);


        //        }
        //        catch (Exception e)
        //        {

        //        }

        //        return new ResponseData<object>(null);
        //    }
        //    public async Task<IResponseData<object>> NearbyAsync(double latitude, double longitude)
        //    {
        //        if (string.IsNullOrEmpty(input))
        //        {
        //            return new ResponseData<object>();
        //        }


        //        var request = new NearbySearchRequest
        //        {
        //            Location = new LatLng(latitude, longitude),
        //            Radius = 10000
        //        };

        //        var response = await CreateService().GetResponseAsync(request);

        //        if (response.Status == ServiceResponseStatus.OverQueryLimit)
        //        {
        //            return new ResponseData<object>(ResponseCode.RequestError, response.ErrorMessage);
        //        }

        //        var result = response.Results;

        //        return new ResponseData<object>(null);
        //    }
        private PlacesService CreateService()
        {
            return new PlacesService(new GoogleSigned(WebApiConstants.GoogleApiKey));
        }

        public async Task<IResponseData<List<PointModel>>> GetPointsByInputText(string text, LatLng location)
        {
            var request = new TextSearchRequest
            {
                Query = text,
                Location = location,
                Radius = 10000 // TODO: TRY RADIUS
            };

            var response = await ProcessPlaceRequestAsync(request);
            if (!response.Success)
            {
                return new ResponseData<List<PointModel>>(response.Code, response.Message);
            }

            var pointModels = response.Data.Select(place => new PointModel
            {
                Name = place.Name,
                Long = place.Geometry.Location.Longitude,
                Lat = place.Geometry.Location.Latitude
            });

            return new ResponseData<List<PointModel>>(pointModels.ToList());
        }

        private async Task<IResponseData<List<PlacesResult>>> ProcessPlaceRequestAsync(PlacesRequest request)
        {
            var service = CreateService();
            var response = await service.GetResponseAsync(request);

            if (response.Status != ServiceResponseStatus.Ok)
            {
                return new ResponseData<List<PlacesResult>> (ResponseCode.RequestError, response.ErrorMessage);
            }

            return new ResponseData<List<PlacesResult>>(new List<PlacesResult>(response.Results));
        }
    }
}
