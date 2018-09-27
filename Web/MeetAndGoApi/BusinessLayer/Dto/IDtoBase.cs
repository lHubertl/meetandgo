using System;
using System.ComponentModel.DataAnnotations;

namespace MeetAndGoApi.BusinessLayer.Dto
{
    public interface IDtoBase
    {
        [Key]
        Guid Id { get; set; }
    }
}
