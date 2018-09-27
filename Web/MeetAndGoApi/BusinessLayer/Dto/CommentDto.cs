using System;
using MeetAndGo.Shared.Models;

namespace MeetAndGoApi.BusinessLayer.Dto
{
    public class CommentDto : CommentModel, IDtoBase
    {
        public Guid Id { get; set; }
    }
}
