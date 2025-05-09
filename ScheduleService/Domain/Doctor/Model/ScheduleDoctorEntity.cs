using Domain.Base.Entity;
using Domain.Schedule.Model;

namespace Domain.Doctor.Model
{
    public class ScheduleDoctorEntity : BaseEntity
    {
        public DateTime DateHour { get; set; }
        public bool IsAllocated { get; set; }
        public Guid DoctorId { get; set; }

        public virtual DoctorEntity Doctor { get; set; }
        public virtual ScheduleEntity Schedule { get; set; }
    }
}
