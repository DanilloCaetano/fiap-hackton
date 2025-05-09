using Domain.Doctor.Model;

namespace Domain.Doctor.Service
{
    public interface IScheduleDoctorService
    {
        Task AddSchedule(ScheduleDoctorEntity scheduleEntity);
        Task UpdateSchedule(Guid scheduleId, ScheduleDoctorEntity scheduleEntity, Guid doctorOperationId);
        Task<IList<ScheduleDoctorEntity>> GetByDoctorId(Guid doctorId);
        Task<IList<ScheduleDoctorEntity>> GetDesalocatedByDoctorIdAndFilter(Guid doctorId, DateTime? initialDate, DateTime? finalDate);
    }
}
