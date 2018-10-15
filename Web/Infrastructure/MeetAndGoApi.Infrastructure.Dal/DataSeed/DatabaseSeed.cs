using System;
using System.Collections.Generic;
using System.Linq;
using MeetAndGo.Shared.Enums;
using MeetAndGoApi.Infrastructure.Dal.Context;
using MeetAndGoApi.Infrastructure.Dal.Dto;
using Microsoft.EntityFrameworkCore.Internal;

namespace MeetAndGoApi.Infrastructure.Dal.DataSeed
{
    internal class DatabaseSeed
    {
        public void InitializeIfNeeded(DatabaseContext context)
        {
            var created = context.Database.EnsureCreated();
            if (!created)
            {
                return;
            }

            if (EnumerableExtensions.Any(context.Users))
            {
                // Db has been seeded
                return;
            }

            var comment = GetMockedCommentDto();
            var eventDto = GetMockedEventDto();
            var eventUser = GetMockedEventUser();
            var point = GetMockedPointDto();
            var user = GetMockedUserDto();
            var anotherUser = GetMockedUserDto();
            var vote = GetMockedVoteDto();

            vote.TargetDto = user;
            vote.UserDto = anotherUser;
            user.VoteDtos = new List<VoteDto> {vote};

            comment.UserDto = user;
            comment.EventDto = eventDto;
            user.CommentDtos = new List<CommentDto> {comment};

            point.EventDto = eventDto;
            eventDto.PointDtos = new List<PointDto> {point};

            eventUser.EventDto = eventDto;
            eventUser.UserDto = user;
            user.EventUsers = new List<EventUser> {eventUser};
            eventDto.EventUsers = user.EventUsers;

            context.Comments.Add(comment);
            context.SaveChanges();
            context.Events.Add(eventDto);
            context.SaveChanges();
            context.Points.Add(point);
            context.SaveChanges();
            context.Votes.Add(vote);
            context.SaveChanges();
            context.Users.Add(user);
            context.SaveChanges();
            context.Users.Add(anotherUser);
            context.SaveChanges();
        }

        #region Get Mocked data

        private CommentDto GetMockedCommentDto()
        {
            return new CommentDto
            {
                Text = "Mocked",
                CommentedIn = DateTimeOffset.Now,
                CommentDtoId = Guid.NewGuid()
            };
        }

        private EventDto GetMockedEventDto()
        {
            return new EventDto
            {
                CreatedTime = DateTimeOffset.Now,
                CurrencyCode = "USD",
                Description = "Test description",
                EventDtoId = Guid.NewGuid(),
                EventState = EventStates.Formation,
                ExpectedRating = 4,
                MaxSeats = 4,
                StartTime = DateTimeOffset.Now,
                TotalPrice = 100,
                Transport = Transports.Car,
                Name = "Test event"
            };
        }

        private UserDto GetMockedUserDto()
        {
            return new UserDto
            {
                Email = "test@gmail.com",
                FirstName = "Peter",
                DateOfBirth = DateTime.Now,
                LanguageCode = "EN",
                LastName = "Peterson",
                MemberRating = 4.9,
                OrganizerRating = 5,
                PhoneNumber = "+39032155135",
                Status = UserStatus.Organizer,
                UserDtoId = Guid.NewGuid()
            };
        }

        private VoteDto GetMockedVoteDto()
        {
            return new VoteDto
            {
                Comment = "Test vote",
                Rating = 4,
                RatingType = UserStatus.Organizer,
                VoteDtoId = Guid.NewGuid()
            };
        }

        private PointDto GetMockedPointDto()
        {
            return new PointDto
            {
                Lat = 4.523234234,
                Long = 0.23553224,
                Name = "TEst Point",
                PointDtoId = Guid.NewGuid()
            };
        }

        private EventUser GetMockedEventUser()
        {
            return new EventUser
            {
                EventUserId = Guid.NewGuid()
            };
        }

        #endregion

    }
}
