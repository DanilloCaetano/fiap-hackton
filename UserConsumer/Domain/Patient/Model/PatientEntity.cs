using Domain.Base.Entity;
using Domain.Doctor.Model;
using Domain.Schedule.Model;

namespace Domain.Patient.Model
{
    public class PatientEntity : BaseEntity
    {
        public string CPF { get; set; }
        public DateTime BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual IList<ScheduleEntity> Schedules { get; set; }
    }
}
