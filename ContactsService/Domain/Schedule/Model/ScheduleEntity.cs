using Domain.Base.Entity;
using Domain.Doctor.Model;
using Domain.Patient.Model;
using Domain.Schedule.Model.Enum;

namespace Domain.Schedule.Model
{
    public class ScheduleEntity : BaseEntity
    {
        public Guid PatientId { get; set; }
        public Guid ScheduleDoctorId { get; set; }
        public DateTime DateHour { get; set; }
        public ScheduleStatusEnum Status { get; set; } = ScheduleStatusEnum.Pending;
        public string? CancellationJustification { get; set; }

        public virtual ScheduleDoctorEntity ScheduleDoctor { get; set; }
        public virtual PatientEntity Patient { get; set; }
    }
}
