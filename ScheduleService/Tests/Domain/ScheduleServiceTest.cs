using Domain.Doctor.Model;
using Domain.Doctor.Repository;
using Domain.Schedule.Model.Enum;
using Domain.Schedule.Model;
using Domain.Schedule.Repository;
using Domain.Schedule.Service;
using Moq;

namespace Tests.Domain
{
    public class ScheduleServiceTest
    {
        private readonly Mock<IScheduleRepository> _scheduleRepositoryMock;
        private readonly Mock<IScheduleDoctorRepository> _scheduleDoctorRepositoryMock;
        private readonly ScheduleService _service;

        public ScheduleServiceTest()
        {
            _scheduleRepositoryMock = new Mock<IScheduleRepository>();
            _scheduleDoctorRepositoryMock = new Mock<IScheduleDoctorRepository>();
            _service = new ScheduleService(_scheduleRepositoryMock.Object, _scheduleDoctorRepositoryMock.Object);
        }

        [Fact]
        public async Task AddSchedule_ShouldAdd_WhenValid()
        {
            var doctorSchedule = new ScheduleDoctorEntity
            {
                Id = Guid.NewGuid(),
                IsAllocated = false,
                DateHour = DateTime.UtcNow.AddHours(1)
            };

            var schedule = new ScheduleEntity
            {
                ScheduleDoctorId = doctorSchedule.Id,
                Status = ScheduleStatusEnum.Pending
            };

            _scheduleDoctorRepositoryMock
                .Setup(repo => repo.GetByIdAsync(doctorSchedule.Id))
                .ReturnsAsync(doctorSchedule);

            _scheduleRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<ScheduleEntity>()))
                .Returns(Task.CompletedTask);

            _scheduleDoctorRepositoryMock
                .Setup(repo => repo.UpdateAsync(It.IsAny<ScheduleDoctorEntity>()))
                .Returns(Task.CompletedTask);

            await _service.AddSchedule(schedule);

            _scheduleRepositoryMock.Verify(r => r.AddAsync(schedule), Times.Once);
            _scheduleDoctorRepositoryMock.Verify(r => r.UpdateAsync(It.Is<ScheduleDoctorEntity>(d => d.IsAllocated)), Times.Once);
        }

        [Fact]
        public async Task AddSchedule_ShouldThrow_WhenScheduleNotFound()
        {
            var schedule = new ScheduleEntity
            {
                ScheduleDoctorId = Guid.NewGuid()
            };

            _scheduleDoctorRepositoryMock
                .Setup(repo => repo.GetByIdAsync(schedule.ScheduleDoctorId))
                .ReturnsAsync((ScheduleDoctorEntity?)null);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.AddSchedule(schedule));
        }

        [Fact]
        public async Task CancelSchedule_ShouldCancelAndFreeSlot()
        {
            var doctorSchedule = new ScheduleDoctorEntity { Id = Guid.NewGuid(), IsAllocated = true };
            var schedule = new ScheduleEntity
            {
                Id = Guid.NewGuid(),
                ScheduleDoctorId = doctorSchedule.Id,
                Status = ScheduleStatusEnum.Approved
            };

            _scheduleRepositoryMock.Setup(r => r.GetByIdAsync(schedule.Id)).ReturnsAsync(schedule);
            _scheduleRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ScheduleEntity>())).Returns(Task.CompletedTask);
            _scheduleDoctorRepositoryMock.Setup(r => r.GetByIdAsync(doctorSchedule.Id)).ReturnsAsync(doctorSchedule);
            _scheduleDoctorRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ScheduleDoctorEntity>())).Returns(Task.CompletedTask);

            await _service.CancelSchedule(schedule.Id, "Motivo qualquer");

            _scheduleRepositoryMock.Verify(r => r.UpdateAsync(It.Is<ScheduleEntity>(s =>
                s.Status == ScheduleStatusEnum.Canceled &&
                s.CancellationJustification == "Motivo qualquer")), Times.Once);

            _scheduleDoctorRepositoryMock.Verify(r => r.UpdateAsync(It.Is<ScheduleDoctorEntity>(d => !d.IsAllocated)), Times.Once);
        }

        [Fact]
        public async Task UpdateScheduleStatus_ShouldChangeStatus_WhenValid()
        {
            var doctorSchedule = new ScheduleDoctorEntity { Id = Guid.NewGuid(), IsAllocated = true };
            var schedule = new ScheduleEntity
            {
                Id = Guid.NewGuid(),
                ScheduleDoctorId = doctorSchedule.Id,
                Status = ScheduleStatusEnum.Pending
            };

            _scheduleRepositoryMock.Setup(r => r.GetByIdAsync(schedule.Id)).ReturnsAsync(schedule);
            _scheduleRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ScheduleEntity>())).Returns(Task.CompletedTask);
            _scheduleDoctorRepositoryMock.Setup(r => r.GetByIdAsync(schedule.ScheduleDoctorId)).ReturnsAsync(doctorSchedule);
            _scheduleDoctorRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<ScheduleDoctorEntity>())).Returns(Task.CompletedTask);

            await _service.UpdateScheduleStatus(schedule.Id, ScheduleStatusEnum.Recused);

            _scheduleRepositoryMock.Verify(r => r.UpdateAsync(It.Is<ScheduleEntity>(s => s.Status == ScheduleStatusEnum.Recused)), Times.Once);
            _scheduleDoctorRepositoryMock.Verify(r => r.UpdateAsync(It.Is<ScheduleDoctorEntity>(d => !d.IsAllocated)), Times.Once);
        }

        [Fact]
        public async Task UpdateScheduleStatus_ShouldThrow_WhenAlreadyCanceled()
        {
            var schedule = new ScheduleEntity
            {
                Id = Guid.NewGuid(),
                Status = ScheduleStatusEnum.Canceled
            };

            _scheduleRepositoryMock.Setup(r => r.GetByIdAsync(schedule.Id)).ReturnsAsync(schedule);

            var ex = await Assert.ThrowsAsync<Exception>(() => _service.UpdateScheduleStatus(schedule.Id, ScheduleStatusEnum.Canceled));
            Assert.Equal("Schedule is already cancelled.", ex.Message);
        }

        [Fact]
        public async Task GetSchedulesByDoctorId_ShouldCallRepository()
        {
            var doctorId = Guid.NewGuid();
            var status = ScheduleStatusEnum.Approved;
            _scheduleRepositoryMock.Setup(r => r.GetSchedulesByDoctorId(doctorId, status)).ReturnsAsync(new List<ScheduleEntity>());

            var result = await _service.GetSchedulesByDoctorId(doctorId, status);

            Assert.NotNull(result);
            _scheduleRepositoryMock.Verify(r => r.GetSchedulesByDoctorId(doctorId, status), Times.Once);
        }
    }
}

