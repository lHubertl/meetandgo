using System;
using System.Collections.Generic;
using System.Linq;
using MeetAndGo.Shared.Enums;
using MeetAndGoApi.Infrastructure.Dal.Context;
using MeetAndGoApi.Infrastructure.Dal.Dto;
using Microsoft.EntityFrameworkCore;
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

            #region Save two users

            var user1 = GetMockedUserDto();
            var user2 = GetMockedUserDto();
            user2.FirstName = "User 2";

            context.Users.Add(user1);
            context.SaveChanges();
            context.Users.Add(user2);
            context.SaveChanges();

            #endregion

            #region Save event by user 1

            var point1 = GetMockedPointDto();
            var point2 = GetMockedPointDto();

            user1 = context.Users.FirstOrDefault();
            var eventDto = GetMockedEventDto();
            eventDto.PointDtos = new List<PointDto> { point1, point2 };
            var eventUser = GetMockedEventUser();
            eventUser.UserDto = user1;
            eventUser.EventDto = eventDto;
            eventDto.EventUsers = new List<EventUser> {eventUser};
            context.Events.Add(eventDto);
            context.SaveChanges();

            #endregion

            #region Add new member to event 

            user2 = context.Users.LastOrDefault();

            eventDto = context.Events.Include(x => x.EventUsers).FirstOrDefault();

            var newEventUser = GetMockedEventUser();
            newEventUser.UserDto = user2;
            newEventUser.EventDto = eventDto;
            
            eventDto.EventUsers.Add(newEventUser);
            context.Events.Update(eventDto);

            #endregion

            #region Save Comment by user1

            eventDto = context.Events.FirstOrDefault();

            user1 = context.Users.FirstOrDefault();
            var comment = GetMockedCommentDto();
            comment.UserDto = user1;
            comment.EventDto = eventDto;
            context.Comments.Add(comment);
            context.SaveChanges();

            #endregion

            #region Vote user2 for user1

            user1 = context.Users.FirstOrDefault();
            user2 = context.Users.FirstOrDefault();
            var vote = GetMockedVoteDto();
            vote.UserDto = user2;
            vote.TargetDto = user1;
            context.Votes.Add(vote);
            context.SaveChanges();

            #endregion
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
                EventDtoId = Guid.NewGuid(),
                CreatedTime = DateTimeOffset.Now,
                CurrencyCode = "USD",
                Description = "Test description",
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
                UserDtoId = Guid.NewGuid(),
                Email = "test@gmail.com",
                FirstName = "Peter",
                DateOfBirth = DateTime.Now,
                LanguageCode = "EN",
                LastName = "Peterson",
                MemberRating = 4.9,
                OrganizerRating = 5,
                PhoneNumber = "+39032155135",
                Status = UserStatus.Organizer
            };
        }

        private VoteDto GetMockedVoteDto()
        {
            return new VoteDto
            {
                VoteDtoId = Guid.NewGuid(),
                Comment = "Test vote",
                Rating = 4,
                RatingType = UserStatus.Organizer
            };
        }

        private PointDto GetMockedPointDto()
        {
            return new PointDto
            {
                PointDtoId = Guid.NewGuid(),
                Lat = 4.523234234,
                Long = 0.23553224,
                Name = "TEst Point"
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
