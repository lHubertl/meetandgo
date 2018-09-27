using System;
using MeetAndGo.Shared.Enums;
using MeetAndGoApi.BusinessLayer.DbContexts;
using MeetAndGoApi.BusinessLayer.Dto;
using Microsoft.EntityFrameworkCore.Internal;

namespace MeetAndGoApi.BusinessLayer.Seed
{
    public class MeetAndGoSeed
    {
        public void Initialize(MeetAndGoContext context)
        {
            var created = context.Database.EnsureCreated();
            if (!created)
            {
                return;
            }

            if (context.Users.Any())
            {
                // Db has been seeded
                return;
            }

            foreach (var user in GetUsers())
            {
                context.Users.Add(user);
            }

            context.SaveChanges();


            foreach (var eventDto in GetEvents())
            {
                context.Events.Add(eventDto);
            }

            context.SaveChanges();
        }

        private UserDto[] GetUsers()
        {
            return new []
            {
                new UserDto { Id = Guid.NewGuid(), FirstName = "Valerii", LastName = "Sovytskyi", DateOfBirth = new DateTime(1995, 4, 30), Email = "revanmvs2@gmail.com", LanguageCode = "EN", MemberRating = 4.9, OrganizerRating = 5.0, PhoneNumber = "+380938632760", Status = UserStatus.User}
            };
        }

        private EventDto[] GetEvents()
        {
            var points = new []
            {
                new PointDto {Long = 24.040686, Lat = 49.809952},
                new PointDto {Long = 24.032185, Lat = 49.829556}
            };
            return new []
            {
                new EventDto { Id = Guid.NewGuid(), CreatedTime = DateTimeOffset.Now, CurrencyCode = "USD", Transport = Transports.Car, Description = "My description", Direction = points, ExpectedRating = 1.0, MaxSeats = 4, StartTime = DateTimeOffset.Now, Name = "From work to home", TotalPrice = 100, EventState = EventStates.Formation}
            };
        }
    }
}
