using System;
using System.Linq;
using AutoMapper;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.Infrastructure.Dto;

namespace MeetAndGoApi.Infrastructure.Dal
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMapForGuidAndString();
            CreateMapForUserModelAndApplicationUser();
            CreateMapForMemberModelAndApplicationUser();
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
                .ForMember(dto => dto.ApplicationUser, model => model.MapFrom(src => src));

            CreateMap<EventModel, EventUser>()
                .ForMember(dto => dto.EventDto, member => member.MapFrom(src => src));

            CreateMap<EventModel, EventDto>()
                .ForMember(dto => dto.PointDtos, model => model.MapFrom(src => src.Direction))
                .ForMember(dto => dto.CommentDtos, model => model.MapFrom(src => src.Comments))
                .ForMember(dto => dto.EventUsers,
                    model => model.MapFrom(src => src.Members.Select(members => members).ToList()));

            CreateMap<EventDto, EventModel>()
                .ForMember(model => model.Id, dto => dto.MapFrom(src => src.EventDtoId))
                .ForMember(model => model.Direction, dto => dto.MapFrom(src => src.PointDtos))
                .ForMember(model => model.Comments, dto => dto.MapFrom(src => src.CommentDtos))
                .ForMember(model => model.Members,
                    dto => dto.MapFrom(src => src.EventUsers.Select(x => x.ApplicationUser).ToList()));
        }

        private void CreateMapForPointModelAndPoindDto()
        {
            CreateMap<PointModel, PointDto>();
            CreateMap<PointDto, PointModel>();
        }

        private void CreateMapForCommentModelAndCommentDto()
        {
            CreateMap<CommentModel, CommentDto>()
                .ForMember(dto => dto.ApplicationUser, model => model.MapFrom(src => src.Author));

            CreateMap<CommentDto, CommentModel>()
                .ForMember(model => model.Author, dto => dto.MapFrom(src => src.ApplicationUser));
        }

        private void CreateMapForVoteModelAndVoteDto()
        {
            CreateMap<VoteModel, VoteDto>()
                .ForMember(dto => dto.TargetDto, model => model.MapFrom(src => src.VoteTarget));

            CreateMap<VoteDto, VoteModel>()
                .ForMember(model => model.VoteTarget, dto => dto.MapFrom(src => src.TargetDto));
        }

        private void CreateMapForUserModelAndApplicationUser()
        {
            CreateMap<UserModel, ApplicationUser>()
                .ForMember(dto => dto.VoteDtos, model => model.MapFrom(src => src.Votes));
            CreateMap<ApplicationUser, UserModel>()
                .ForMember(model => model.Votes, dto => dto.MapFrom(src => src.VoteDtos));
        }

        private void CreateMapForMemberModelAndApplicationUser()
        {
            CreateMap<MemberModel, ApplicationUser>();
            CreateMap<ApplicationUser, MemberModel>();
        }
    }
}
