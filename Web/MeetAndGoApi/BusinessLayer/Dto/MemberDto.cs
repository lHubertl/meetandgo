using System;
using MeetAndGo.Shared.Models;

namespace MeetAndGoApi.BusinessLayer.Dto
{
    public class MemberDto : MemberModel, IDtoBase
    {
        public Guid Id { get; set; }
    }
}
