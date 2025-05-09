using Domain.Doctor.Model;
using Domain.Doctor.Repository;
using Domain.Doctor.Service;
using Moq;
using System.Linq.Expressions;

namespace Tests.Domain
{
    public class ScheduleDoctorServiceTest
    {
        private readonly Mock<IScheduleDoctorRepository> _repositoryMock;
        private readonly ScheduleDoctorService _service;

        public ScheduleDoctorServiceTest()
        {
            _repositoryMock = new Mock<IScheduleDoctorRepository>();
            _service = new ScheduleDoctorService(_repositoryMock.Object);
        }

        [Fact]
        public async Task AddSchedule_ShouldCallRepository()
        {
            var schedule = new ScheduleDoctorEntity { Id = Guid.NewGuid() };

            await _service.AddSchedule(schedule);

            _repositoryMock.Verify(r => r.AddAsync(schedule), Times.Once);
        }

        [Fact]
        public async Task GetByDoctorId_ShouldReturnSchedules()
        {
            var doctorId = Guid.NewGuid();
            var expected = new List<ScheduleDoctorEntity>
            {
                new ScheduleDoctorEntity { DoctorId = doctorId }
            };

            _repositoryMock
                .Setup(r => r.GetAsync(It.IsAny<Expression<Func<ScheduleDoctorEntity, bool>>>()))
                .ReturnsAsync(expected);

            var result = await _service.GetByDoctorId(doctorId);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(doctorId, result[0].DoctorId);
        }

        [Fact]
        public async Task GetDesalocatedByDoctorIdAndFilter_ShouldReturnFilteredSchedules()
        {
            var doctorId = Guid.NewGuid();
            var initialDate = DateTime.UtcNow.Date;
            var finalDate = initialDate.AddDays(1);

            var expected = new List<ScheduleDoctorEntity>
            {
                new ScheduleDoctorEntity
                {
                    DoctorId = doctorId,
                    IsAllocated = false,
                    DateHour = initialDate.AddHours(10)
                }
            };

            _repositoryMock
                .Setup(r => r.GetAsync(It.IsAny<Expression<Func<ScheduleDoctorEntity, bool>>>()))
                .ReturnsAsync(expected);

            var result = await _service.GetDesalocatedByDoctorIdAndFilter(doctorId, initialDate, finalDate);

            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task UpdateSchedule_ShouldUpdate_WhenValid()
        {
            var scheduleId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();

            var existing = new ScheduleDoctorEntity
            {
                Id = scheduleId,
                DoctorId = doctorId
            };

            var updated = new ScheduleDoctorEntity
            {
                Id = scheduleId,
                DoctorId = doctorId,
                DateHour = DateTime.UtcNow.AddHours(2)
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(scheduleId)).ReturnsAsync(existing);
            _repositoryMock.Setup(r => r.UpdateAsync(updated)).Returns(Task.CompletedTask);

            await _service.UpdateSchedule(scheduleId, updated, doctorId);

            _repositoryMock.Verify(r => r.UpdateAsync(updated), Times.Once);
        }

        [Fact]
        public async Task UpdateSchedule_ShouldThrow_WhenScheduleNotFound()
        {
            var scheduleId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();

            _repositoryMock.Setup(r => r.GetByIdAsync(scheduleId)).ReturnsAsync((ScheduleDoctorEntity?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _service.UpdateSchedule(scheduleId, new ScheduleDoctorEntity(), doctorId));
        }

        [Fact]
        public async Task UpdateSchedule_ShouldThrow_WhenDoctorIdMismatch()
        {
            var scheduleId = Guid.NewGuid();
            var doctorId = Guid.NewGuid();
            var anotherDoctorId = Guid.NewGuid();

            var existing = new ScheduleDoctorEntity
            {
                Id = scheduleId,
                DoctorId = anotherDoctorId
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(scheduleId)).ReturnsAsync(existing);

            await Assert.ThrowsAsync<Exception>(() =>
                _service.UpdateSchedule(scheduleId, new ScheduleDoctorEntity(), doctorId));
        }
    }
}
