using System;
using System.Linq;
using AutoMapper;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.Infrastructure.Dal.Dto;

namespace MeetAndGoApi.Infrastructure.Dal
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMapForGuidAndString();
            CreateMapForUserModelAndUserDto();
            CreateMapForMemberModelAndUserDto();
            CreateMapForPointModelAndPoindDto();
            CreateMapForCommentModelAndCommentDto();
            CreateMapForVoteModelAndVoteDto();
            CreateMapForEventModelAndEventDto();
        }

        private void CreateMapForGuidAndString()
        {
            CreateMap<string, Guid>().ConvertUsing(Guid.Parse);
            CreateMap<string, Guid?>().ConvertUsing(s => string.IsNullOrWhiteSpace(s) ? (Guid?) null : Guid.Parse(s));
            CreateMap<Guid?, string>().ConvertUsing(g => g?.ToString("N"));
            CreateMap<Guid, string>().ConvertUsing(g => g.ToString("N"));
        }

        private void CreateMapForEventModelAndEventDto()
        {
            CreateMap<MemberModel, EventUser>()
                .ForMember(dto => dto.UserDto, model => model.MapFrom(src => src));

            CreateMap<EventModel, EventUser>()
                .ForMember(dto => dto.EventDto, member => member.MapFrom(src => src));

            CreateMap<EventModel, EventDto>()
                .ForMember(dto => dto.PointDtos, model => model.MapFrom(src => src.Direction))
                .ForMember(dto => dto.CommentDtos, model => model.MapFrom(src => src.Comments))
                .ForMember(dto => dto.EventUsers,
                    model => model.MapFrom(src => src.Members.Select(members => members).ToList()));

            CreateMap<EventDto, EventModel>()
                .ForMember(model => model.Direction, dto => dto.MapFrom(src => src.PointDtos))
                .ForMember(model => model.Comments, dto => dto.MapFrom(src => src.CommentDtos))
                .ForMember(model => model.Members,
                    dto => dto.MapFrom(src => src.EventUsers.Select(x => x.UserDto).ToList()));
        }

        private void CreateMapForPointModelAndPoindDto()
        {
            CreateMap<PointModel, PointDto>();
            CreateMap<PointDto, PointModel>();
        }

        private void CreateMapForCommentModelAndCommentDto()
        {
            CreateMap<CommentModel, CommentDto>()
                .ForMember(dto => dto.UserDto, model => model.MapFrom(src => src.Author));

            CreateMap<CommentDto, CommentModel>();
        }

        private void CreateMapForVoteModelAndVoteDto()
        {
            CreateMap<VoteModel, VoteDto>()
                .ForMember(dto => dto.TargetDto, model => model.MapFrom(src => src.VoteTarget));

            CreateMap<VoteDto, VoteModel>();
        }

        private void CreateMapForUserModelAndUserDto()
        {
            CreateMap<UserModel, UserDto>()
                .ForMember(dto => dto.VoteDtos, model => model.MapFrom(src => src.Votes));
            CreateMap<UserDto, UserModel>();
        }

        private void CreateMapForMemberModelAndUserDto()
        {
            CreateMap<MemberModel, UserDto>();
            CreateMap<UserDto, MemberModel>();
        }
    }
}
