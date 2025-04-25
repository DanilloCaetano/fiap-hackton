using Domain.Doctor.Model;

namespace Domain.Doctor.Service
{
    public interface IScheduleDoctorService
    {
        Task AddSchedule(ScheduleDoctorEntity scheduleEntity);
        Task UpdateSchedule(ScheduleDoctorEntity scheduleEntity);
        Task<IList<ScheduleDoctorEntity>> GetByDoctorId(Guid doctorId);
    }
}
