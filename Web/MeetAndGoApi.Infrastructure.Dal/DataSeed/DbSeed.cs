using System;
using System.Collections.Generic;
using System.Linq;
using MeetAndGo.Shared.Enums;
using MeetAndGoApi.Infrastructure.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace MeetAndGoApi.Infrastructure.Dal.DataSeed
{
    internal class DbSeed
    {
        public void InitializeIfNeeded(Context.DbContext context)
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

            var user1 = GetMockedApplicationUser();
            var user2 = GetMockedApplicationUser();
            user2.FirstName = "User 2";

            context.Users.Add(user1);
            context.SaveChanges();
            context.Users.Add(user2);
            context.SaveChanges();

            #endregion

            #region Save event by user 1

            var point1 = GetMockedPointDto(49.809711, 24.040235);
            var point2 = GetMockedPointDto(49.839282, 24.033116);

            user1 = context.Users.FirstOrDefault();
            var eventDto = GetMockedEventDto();
            eventDto.PointDtos = new List<PointDto> { point1, point2 };
            var eventUser = GetMockedEventUser();
            eventUser.ApplicationUser = user1;
            eventUser.EventDto = eventDto;
            eventDto.EventUsers = new List<EventUser> {eventUser};
            context.Events.Add(eventDto);
            context.SaveChanges();

            #endregion

            #region Add new member to event 

            user2 = context.Users.LastOrDefault();

            eventDto = context.Events.Include(x => x.EventUsers).FirstOrDefault();

            var newEventUser = GetMockedEventUser();
            newEventUser.ApplicationUser = user2;
            newEventUser.EventDto = eventDto;
            
            eventDto.EventUsers.Add(newEventUser);
            context.Events.Update(eventDto);

            #endregion

            #region Save Comment by user1

            eventDto = context.Events.FirstOrDefault();

            user1 = context.Users.FirstOrDefault();
            var comment = GetMockedCommentDto();
            comment.ApplicationUser = user1;
            comment.EventDto = eventDto;
            context.Comments.Add(comment);
            context.SaveChanges();

            #endregion

            #region Vote user2 for user1

            user1 = context.Users.FirstOrDefault();
            user2 = context.Users.FirstOrDefault();
            var vote = GetMockedVoteDto();
            vote.ApplicationUser = user2;
            vote.TargetDto = user1;
            context.Votes.Add(vote);
            context.SaveChanges();

            #endregion

            #region MyRegion

            user1 = context.Users.FirstOrDefault();

            var file = GetMockedFile();
            file.ApplicationUser = user1;
            context.Files.Add(file);
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

        private ApplicationUser GetMockedApplicationUser()
        {
            return new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "test@gmail.com",
                FirstName = "Peter",
                DateOfBirth = DateTime.Now,
                LanguageCode = "EN",
                LastName = "Peterson",
                MemberRating = 4.9,
                OrganizerRating = 5,
                PhoneNumber = "39032155135",
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

        private PointDto GetMockedPointDto(double lat, double longi)
        {
            return new PointDto
            {
                PointDtoId = Guid.NewGuid(),
                Lat = lat,
                Long = longi,
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

        private FileDto GetMockedFile()
        {
            return new FileDto
            {
                FileDtoId = Guid.NewGuid(),
                Name = "Photo",
                Path =
                    "https://scontent.flwo1-1.fna.fbcdn.net/v/t1.0-9/40042014_2032196270206068_4734283304386166784_n.jpg?_nc_cat=100&_nc_ht=scontent.flwo1-1.fna&oh=31e093ff4718c7c5064dfb45b8f18c7a&oe=5C8B9EBE"
            };
        }

        #endregion

    }
}
