using Domain.Doctor.Repository;
using Domain.Schedule.Model;
using Domain.Schedule.Model.Enum;
using Domain.Schedule.Repository;

namespace Domain.Schedule.Service
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IScheduleDoctorRepository _scheduleDoctorRepository;

        public ScheduleService(IScheduleRepository scheduleRepository, IScheduleDoctorRepository scheduleDoctorRepository)
        {
            _scheduleRepository = scheduleRepository;
            _scheduleDoctorRepository = scheduleDoctorRepository;
        }

        public async Task AddSchedule(ScheduleEntity scheduleEntity)
        {
            var existingDoctorSchedule = await _scheduleDoctorRepository.GetByIdAsync(scheduleEntity.ScheduleDoctorId);
            if (existingDoctorSchedule == null)
            {
                throw new KeyNotFoundException($"Doctor schedule with ID {scheduleEntity.ScheduleDoctorId} not found.");
            }

            if (existingDoctorSchedule.IsAllocated)
            {
                throw new InvalidOperationException($"Doctor schedule with ID {scheduleEntity.ScheduleDoctorId} is already allocated.");
            }

            if (existingDoctorSchedule.DateHour < DateTime.UtcNow)
            {
                throw new ArgumentException("Start date and time must be in the future.");
            }

            await _scheduleRepository.AddAsync(scheduleEntity);

            existingDoctorSchedule.IsAllocated = true;
            await _scheduleDoctorRepository.UpdateAsync(existingDoctorSchedule);
        }

        public async Task CancelSchedule(Guid scheduleId, string justification)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(scheduleId);
            if (schedule == null)
            {
                throw new KeyNotFoundException($"Schedule with ID {scheduleId} not found.");
            }

            schedule.Status = ScheduleStatusEnum.Canceled;
            schedule.CancellationJustification = justification;
            await _scheduleRepository.UpdateAsync(schedule);

            var existingDoctorSchedule = await _scheduleDoctorRepository.GetByIdAsync(schedule.ScheduleDoctorId);
            existingDoctorSchedule.IsAllocated = false;
            await _scheduleDoctorRepository.UpdateAsync(existingDoctorSchedule);
        }

        public async Task<IList<ScheduleEntity>> GetSchedulesByDoctorId(Guid doctorId, ScheduleStatusEnum? scheduleStatusEnum)
        {
            return await _scheduleRepository.GetSchedulesByDoctorId(doctorId, scheduleStatusEnum);
        }

        public async Task UpdateScheduleStatus(Guid scheduleId, ScheduleStatusEnum newStatus)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(scheduleId);
            if (schedule == null)
            {
                throw new KeyNotFoundException($"Schedule with ID {scheduleId} not found.");
            }

            if (schedule.Status == ScheduleStatusEnum.Canceled)
                throw new Exception($"Schedule is already cancelled.");

            if (schedule.Status == newStatus) return;

            if (newStatus == ScheduleStatusEnum.Recused || newStatus == ScheduleStatusEnum.Canceled)
            {
                var existingDoctorSchedule = await _scheduleDoctorRepository.GetByIdAsync(schedule.ScheduleDoctorId);
                if (existingDoctorSchedule == null)
                {
                    throw new KeyNotFoundException($"Doctor schedule with ID {schedule.ScheduleDoctorId} not found.");
                }

                existingDoctorSchedule.IsAllocated = false;
                await _scheduleDoctorRepository.UpdateAsync(existingDoctorSchedule);
            }

            schedule.Status = newStatus;
            await _scheduleRepository.UpdateAsync(schedule);
        }
    }
}
