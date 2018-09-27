using System;
using MeetAndGo.Shared.Models;

namespace MeetAndGoApi.BusinessLayer.Dto
{
    public class VoteDto : VoteModel, IDtoBase
    {
        public Guid Id { get; set; }
    }
}
