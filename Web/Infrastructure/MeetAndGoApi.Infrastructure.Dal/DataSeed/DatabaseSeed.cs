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

            UserDto mockedUser = null;
            foreach (var user in GetUsers())
            {
                context.Users.Add(user);
                mockedUser = user;
            }

            context.SaveChanges();

            foreach (var eventDto in GetEvents())
            {
                eventDto.EventUsers = new List<EventUser>
                {
                    new EventUser
                    {
                        //EventUserId = Guid.NewGuid(),
                        UserDto = mockedUser,
                        EventDto = eventDto,
                    }
                };
                context.Events.Add(eventDto);
            }

            context.SaveChanges();

            foreach (var vote in GetVotes())
            {
                vote.UserDto = mockedUser;
                context.Votes.Add(vote);
            }
            
            context.SaveChanges();
        }

        private UserDto[] GetUsers()
        {
            return new[]
            {
                new UserDto
                {
                    //UserDtoId = Guid.NewGuid(),
                    FirstName = "Valerii",
                    LastName = "Sovytskyi",
                    DateOfBirth = new DateTime(1995, 4, 30),
                    Email = "revanmvs2@gmail.com",
                    LanguageCode = "EN",
                    MemberRating = 4.9,
                    OrganizerRating = 5.0,
                    PhoneNumber = "+380938632760",
                    Status = UserStatus.User
                }
            };
        }

        private List<EventDto> GetEvents()
        {
            var points = new List<PointDto>
            {
                new PointDto
                {
                    //PointDtoId = Guid.NewGuid(),
                    Long = 24.040686,
                    Lat = 49.809952
                },
                new PointDto
                {
                    //PointDtoId = Guid.NewGuid(),
                    Long = 24.032185,
                    Lat = 49.829556
                }
            };

            return new List<EventDto>
            {
                new EventDto
                {
                    //EventDtoId = Guid.NewGuid(),
                    PointDtos = points,
                    CreatedTime = DateTimeOffset.Now,
                    CurrencyCode = "USD",
                    Transport = Transports.Car,
                    Description = "My description",
                    ExpectedRating = 1.0,
                    MaxSeats = 4,
                    StartTime = DateTimeOffset.Now,
                    Name = "From work to home",
                    TotalPrice = 100,
                    EventState = EventStates.Formation
                }
            };
        }

        private List<VoteDto> GetVotes()
        {
            return new List<VoteDto>
            {
                new VoteDto
                {
                    Comment = "Smth",
                    Rating = 4,
                    RatingType = UserStatus.Member,
                }
            };
        }
    }
}
