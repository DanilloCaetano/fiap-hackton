using Domain.Doctor.Model;

namespace Domain.Doctor.Service
{
    public class ScheduleDoctorService : IScheduleDoctorService
    {
        public Task AddSchedule(ScheduleDoctorEntity scheduleEntity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ScheduleDoctorEntity>> GetByDoctorId(Guid doctorId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSchedule(ScheduleDoctorEntity scheduleEntity)
        {
            throw new NotImplementedException();
        }
    }
}
