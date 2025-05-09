using Domain.Doctor.Model;
using Domain.Doctor.Repository;
using System.Linq.Expressions;

namespace Domain.Doctor.Service
{
    public class ScheduleDoctorService : IScheduleDoctorService
    {
        private readonly IScheduleDoctorRepository _scheduleDoctorRepository;

        public ScheduleDoctorService(IScheduleDoctorRepository scheduleDoctorRepository)
        {
            _scheduleDoctorRepository = scheduleDoctorRepository;
        }

        public async Task AddSchedule(ScheduleDoctorEntity scheduleEntity)
        {
            await _scheduleDoctorRepository.AddAsync(scheduleEntity);
        }

        public async Task<IList<ScheduleDoctorEntity>> GetByDoctorId(Guid doctorId)
        {
            return await _scheduleDoctorRepository.GetAsync(s => s.DoctorId == doctorId);
        }

        public async Task<IList<ScheduleDoctorEntity>> GetDesalocatedByDoctorIdAndFilter(Guid doctorId, DateTime? initialDate, DateTime? finalDate)
        {
            Expression<Func<ScheduleDoctorEntity, bool>> query = s => 
                s.DoctorId == doctorId && 
                !s.IsAllocated && 
                (initialDate.HasValue && finalDate.HasValue) ? (s.DateHour >= initialDate.Value.Date && s.DateHour <= finalDate.Value.Date) : true;
            
            return await _scheduleDoctorRepository.GetAsync(query);
        }

        public async Task UpdateSchedule(Guid scheduleId, ScheduleDoctorEntity scheduleEntity, Guid doctorOperationId)
        {
            var existingSchedule = await _scheduleDoctorRepository.GetByIdAsync(scheduleId);
            if (existingSchedule == null)
            {
                throw new KeyNotFoundException($"Schedule with ID {scheduleId} not found.");
            }

            if (existingSchedule.DoctorId != doctorOperationId)
            {
                throw new Exception($"Doctor ID is not same of exists schedule.");
            }

            await _scheduleDoctorRepository.UpdateAsync(scheduleEntity);
        }
    }
}
