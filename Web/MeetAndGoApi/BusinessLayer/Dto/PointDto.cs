using System;
using MeetAndGo.Shared.Models;

namespace MeetAndGoApi.BusinessLayer.Dto
{
    public class PointDto : PointModel, IDtoBase
    {
        public Guid Id { get; set; }
    }
}
