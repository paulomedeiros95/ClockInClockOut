using ClockInClockOut_Domain.Domains;
using ClockInClockOut_Infra.Interfaces;
using ClockInClockOut_Services.Exceptions;
using ClockInClockOut_Services.Services;
using Moq;
using System.Linq.Expressions;

namespace ClockInClockOut_Tests
{

    [TestFixture]
    public class TimeServiceTest
    {
        private TimeService _timeService;
        private Mock<IBaseRepository<TimeDomain>> _timeRepositoryMock;
        private Mock<IBaseRepository<UserDomain>> _userRepositoryMock;
        private Mock<IBaseRepository<ProjectDomain>> _projectRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _timeRepositoryMock = new Mock<IBaseRepository<TimeDomain>>();
            _userRepositoryMock = new Mock<IBaseRepository<UserDomain>>();
            _projectRepositoryMock = new Mock<IBaseRepository<ProjectDomain>>();
            _timeService = new TimeService(_timeRepositoryMock.Object, _userRepositoryMock.Object, _projectRepositoryMock.Object);
        }

        [Test]
        public async Task Add_ShouldThrowRequiredFieldException_WhenStartedAtIsMinValue()
        {
            // Arrange
            var time = new TimeDomain
            {
                UserID = 1,
                ProjectID = 1,
                StartedAt = DateTime.MinValue,
                EndedAt = DateTime.Now
            };

            // Act & Assert
            Assert.ThrowsAsync<RequiredFieldException>(async () => await _timeService.Add(time));
        }

        [Test]
        public async Task Add_ShouldThrowRequiredFieldException_WhenUserIDIsZero()
        {
            // Arrange
            var time = new TimeDomain
            {
                UserID = 0,
                ProjectID = 1,
                StartedAt = DateTime.Now,
                EndedAt = DateTime.Now.AddDays(1)
            };

            // Act & Assert
            Assert.ThrowsAsync<RequiredFieldException>(async () => await _timeService.Add(time));
        }

        [Test]
        public async Task Add_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            _userRepositoryMock
                .Setup(x => x.SelectFirstBy(It.IsAny<Expression<Func<UserDomain, bool>>>()))
                .ReturnsAsync((UserDomain)null);
            var time = new TimeDomain
            {
                UserID = 1,
                ProjectID = 1,
                StartedAt = DateTime.Now,
                EndedAt = DateTime.Now.AddDays(1)
            };

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _timeService.Add(time));
        }

        [Test]
        public async Task Add_ShouldThrowDuplicateItemException_WhenAddingDuplicateTimePosting()
        {
            // Arrange
            _userRepositoryMock
                .Setup(x => x.SelectFirstBy(It.IsAny<Expression<Func<UserDomain, bool>>>()))
                .ReturnsAsync(new UserDomain());
            _projectRepositoryMock
                .Setup(x => x.SelectFirstBy(It.IsAny<Expression<Func<ProjectDomain, bool>>>()))
                .ReturnsAsync(new ProjectDomain());
            _timeRepositoryMock
                .Setup(x => x.Select(It.IsAny<Expression<Func<TimeDomain, bool>>>()))
                .ReturnsAsync(new List<TimeDomain> { new TimeDomain() });
            var time = new TimeDomain
            {
                UserID = 1,
                ProjectID = 1,
                StartedAt = DateTime.Now,
                EndedAt = DateTime.Now.AddDays(1)
            };

            // Act & Assert
            Assert.ThrowsAsync<DuplicateItemException>(async () => await _timeService.Add(time));
        }

        [Test]
        public async Task Add_ShouldCallInsertAndCommitAsync_WhenTimeIsValid()
        {
            // Arrange
            _userRepositoryMock
                .Setup(x => x.SelectFirstBy(It.IsAny<Expression<Func<UserDomain, bool>>>()))
                .ReturnsAsync(new UserDomain());
            _projectRepositoryMock
                .Setup(x => x.SelectFirstBy(It.IsAny<Expression<Func<ProjectDomain, bool>>>()))
                .ReturnsAsync(new ProjectDomain());
            _timeRepositoryMock
                .Setup(x => x.Select(It.IsAny<Expression<Func<TimeDomain, bool>>>()))
                .ReturnsAsync(new List<TimeDomain>());
            var time = new TimeDomain
            {
                UserID = 1,
                ProjectID = 1,
                StartedAt = DateTime.Now,
                EndedAt = DateTime.Now.AddDays(1)
            };

            // Act
            await _timeService.Add(time);

            // Assert
            _timeRepositoryMock.Verify(x => x.Insert(It.IsAny<TimeDomain>()), Times.Once);
            _timeRepositoryMock.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Test]
        public async Task Update_ShouldThrowNotFoundException_WhenTimePostingDoesNotExist()
        {
            // Arrange
            _timeRepositoryMock
                .Setup(x => x.SelectFirstBy(It.IsAny<Expression<Func<TimeDomain, bool>>>()))
                .ReturnsAsync((TimeDomain)null);
            var time = new TimeDomain
            {
                ID = 1,
                UserID = 1,
                ProjectID = 1,
                StartedAt = DateTime.Now,
                EndedAt = DateTime.Now.AddDays(1)
            };

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _timeService.Update(time));
        }
    }
}