using System;
using AutoMapper;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.Infrastructure.Dal.Dto;

namespace MeetAndGoApi.Infrastructure.Dal
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            // String - Guid
            CreateMap<string, Guid>().ConvertUsing(Guid.Parse);
            CreateMap<string, Guid?>().ConvertUsing(s => string.IsNullOrWhiteSpace(s) ? (Guid?)null : Guid.Parse(s));
            CreateMap<Guid?, string>().ConvertUsing(g => g?.ToString("N"));
            CreateMap<Guid, string>().ConvertUsing(g => g.ToString("N"));

            CreateMap<EventModel, EventDto>()
                .ForMember(dto => dto.PointDtos, model => model.MapFrom(src => src.Direction))
                .ForMember(dto => dto.CommentDtos, model => model.MapFrom(src => src.Comments));

            CreateMap<EventDto, EventModel>();

            CreateMap<PointModel, PointDto>();
            CreateMap<PointDto, PointModel>();

            CreateMap<CommentModel, CommentDto>()
                .ForMember(dto => dto.UserDto, model => model.MapFrom(src => src.Author));

            CreateMap<CommentDto, CommentModel>();

            CreateMap<VoteModel, VoteDto>()
                .ForMember(dto => dto.TargetDto, model => model.MapFrom(src => src.VoteTarget));

            CreateMap<VoteDto, VoteModel>();

            // UserModel -> UserDto
            CreateMap<UserModel, UserDto>()
                .ForMember(dto => dto.VoteDtos, model => model.MapFrom(src => src.Votes));
            CreateMap<UserDto, UserModel>();

            CreateMap<MemberModel, UserDto>();
            CreateMap<UserDto, MemberModel>();
        }
    }
}
