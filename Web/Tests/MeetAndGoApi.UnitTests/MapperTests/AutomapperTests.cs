using System;
using System.Collections.Generic;
using AutoMapper;
using MeetAndGo.Shared.Enums;
using MeetAndGo.Shared.Models;
using MeetAndGoApi.Infrastructure.Dal;
using MeetAndGoApi.Infrastructure.Dal.Dto;
using NUnit.Framework;

namespace MeetAndGoApi.UnitTests.MapperTests
{
    [TestFixture]
    public class AutomapperTests
    {
        #region Private fields

        private readonly IMapper _mockedMapper;
        
        #endregion

        #region Constructor

        public AutomapperTests()
        {
            var profile = new DomainProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mockedMapper = new Mapper(configuration);
        }

        #endregion

        #region Test Comment

        [Test]
        public void Mapper_MapCommentModel_MappedCommentDto()
        {
            // Arrange
            var model = GetMockedCommentModel();
            model.Author = GetMockedUserModel();

            // Act
            var actual = _mockedMapper.Map<CommentDto>(model);

            // Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.UserDto);
        }

        [Test]
        public void Mapper_MapCommentDto_MappedCommentModel()
        {
            // Arrange
            var dto = GetMockedCommentDto();
            dto.UserDto = GetMockedUserDto();
            dto.UserDto.VoteDtos = new List<VoteDto> { GetMockedVoteDto() };
            dto.EventDto = GetMockedEventDto();
            dto.EventDto.CommentDtos = new List<CommentDto> { dto };
            var eventDto = GetMockedEventUser();
            eventDto.UserDto = dto.UserDto;
            eventDto.EventDto = dto.EventDto;
            dto.EventDto.EventUsers = new List<EventUser> { eventDto };

            // Act
            var actual = _mockedMapper.Map<CommentModel>(dto);

            // Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.Author);
        }


        #endregion

        #region Test Vote
        
        [Test]
        public void Mapper_MapVoteModel_MappedVoteDto()
        {
            // Arrange
            var model = GetMockedVoteModel();
            model.VoteTarget = GetMockedUserModel();

            // Act
            var actual = _mockedMapper.Map<VoteDto>(model);

            // Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.TargetDto);
        }

        [Test]
        public void Mapper_MapVoteDto_MappedVoteModel()
        {
            // Arrange
            var dto = GetMockedVoteDto();
            dto.UserDto = GetMockedUserDto();
            dto.UserDto.VoteDtos = new List<VoteDto>{dto};
            dto.TargetDto = GetMockedUserDto();

            // Act
            var actual = _mockedMapper.Map<VoteModel>(dto);

            // Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.VoteTarget);
        }

        #endregion

        #region Test Point

        [Test]
        public void Mapper_MapPointModel_MappedPointDto()
        {
            // Arrange
            var model = GetMockedPointModel();

            // Act
            var actual = _mockedMapper.Map<PointDto>(model);

            // Assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Mapper_MapPointDto_MappedPointModel()
        {
            // Arrange
            var dto = GetMockedPointDto();
            dto.EventDto = GetMockedEventDto();
            dto.EventDto.PointDtos = new List<PointDto>{dto};

            // Act
            var actual = _mockedMapper.Map<PointModel>(dto);

            // Assert
            Assert.NotNull(actual);
        }

        #endregion

        #region Test User

        [Test]
        public void Mapper_MapUserModel_MappedUserDto()
        {
            // Arrange
            var model = GetMockedUserModel();
            var vote = GetMockedVoteModel();
            vote.VoteTarget = model;
            model.Votes = new List<VoteModel>{ vote };
            vote.VoteTarget = GetMockedUserModel();

            // Act
            var actual = _mockedMapper.Map<UserDto>(model);

            // Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.VoteDtos);
        }

        [Test]
        public void Mapper_MapUserDto_MappedUserModel()
        {
            // Arrange
            var dto = GetMockedUserDto();
            var vote = GetMockedVoteDto();
            vote.TargetDto = dto;
            dto.VoteDtos = new List<VoteDto>{vote};

            // Act
            var actual = _mockedMapper.Map<UserModel>(dto);

            // Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.Votes);
        }

        #endregion
        
        #region Test Member

        [Test]
        public void Mapper_MapMemberModel_MappedUserDto()
        {
            // Arrange
            var model = GetMockedMemberModel();

            // Act
            var actual = _mockedMapper.Map<UserDto>(model);

            // Assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Mapper_MapUserDto_MappedMemberModel()
        {
            // Arrange
            var dto = GetMockedUserDto();

            // Act
            var actual = _mockedMapper.Map<MemberModel>(dto);

            // Assert
            Assert.NotNull(actual);
        }

        #endregion

        #region Test Event

        [Test]
        public void Mapper_MapEventModel_MappedEventDto()
        {
            // Arrange
            var model = GetMockedEventModel();
            var member = GetMockedMemberModel();
            model.Members = new List<MemberModel> { member };
            var point = GetMockedPointModel();
            model.Direction = new List<PointModel> { point };
            var comment = GetMockedCommentModel();
            comment.Author = member;
            model.Comments = new List<CommentModel> { comment };

            // Act
            var actual = _mockedMapper.Map<EventDto>(model);

            // Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.CommentDtos);
            Assert.NotNull(actual.PointDtos);
            Assert.NotNull(actual.EventUsers);

            foreach (var eventUser in actual.EventUsers)
            {
                Assert.NotNull(eventUser.UserDto);
            }
        }

        [Test]
        public void Mapper_MapEventDto_MappedEventModel()
        {
            // Arrange
            var dto = GetMockedEventDto();
            var user = GetMockedUserDto();
            var eventUser = GetMockedEventUser();
            eventUser.EventDto = dto;
            eventUser.UserDto = user;
            user.EventUsers = new List<EventUser> { eventUser };
            dto.EventUsers = new List<EventUser> {eventUser};
            
            // Act
            var actual = _mockedMapper.Map<EventModel>(dto);

            // Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.Comments);
            Assert.NotNull(actual.Direction);
            Assert.NotNull(actual.Members);
            foreach (var member in actual.Members)
            {
                Assert.NotNull(member.Id);
            }
        }

        #endregion


        #region Get Mocked Dto objects

        private CommentDto GetMockedCommentDto()
        {
            return new CommentDto
            {
                Text = "Mocked",
                CommentedIn = DateTimeOffset.Now,
                CommentDtoId = Guid.Empty
            };
        }

        private EventDto GetMockedEventDto()
        {
            return new EventDto
            {
                CreatedTime = DateTimeOffset.Now,
                CurrencyCode = "USD",
                Description = "Test description",
                EventDtoId = Guid.Empty,
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
                UserDtoId = Guid.Empty
            };
        }

        private VoteDto GetMockedVoteDto()
        {
            return new VoteDto
            {
                Comment = "Test vote",
                Rating = 4,
                RatingType = UserStatus.Organizer,
                VoteDtoId = Guid.Empty
            };
        }

        private PointDto GetMockedPointDto()
        {
            return new PointDto
            {
                Lat = 4.523234234,
                Long = 0.23553224,
                Name = "TEst Point",
                PointDtoId = Guid.Empty,
            };
        }

        private EventUser GetMockedEventUser()
        {
            return new EventUser
            {
                EventUserId = Guid.Empty
            };
        }

        #endregion

        #region Get Mocked Model objects

        private CommentModel GetMockedCommentModel()
        {
            return new CommentModel
            {
                Text = "Mocked",
                CommentedIn = DateTimeOffset.Now
            };
        }

        private EventModel GetMockedEventModel()
        {
            return new EventModel
            {
                CreatedTime = DateTimeOffset.Now,
                CurrencyCode = "USD",
                Description = "Test description",
                EventState = EventStates.Formation,
                ExpectedRating = 4,
                MaxSeats = 4,
                StartTime = DateTimeOffset.Now,
                TotalPrice = 100,
                Transport = Transports.Car,
                Name = "Test event",
                Id = Guid.Empty.ToString()
            };
        }

        private UserModel GetMockedUserModel()
        {
            return new UserModel
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
                Id = Guid.Empty.ToString()
            };
        }

        private MemberModel GetMockedMemberModel()
        {
            return GetMockedUserModel();
        }

        private VoteModel GetMockedVoteModel()
        {
            return new VoteModel
            {
                Comment = "Test vote",
                Rating = 4,
                RatingType = UserStatus.Organizer
            };
        }

        private PointModel GetMockedPointModel()
        {
            return new PointModel
            {
                Lat = 4.523234234,
                Long = 0.23553224,
                Name = "TEst Point"
            };
        }
        
        #endregion
    }
}
