using Domain.Base.Entity;
using Domain.Doctor.Model.Enum;

namespace Domain.Doctor.Model
{
    public class DoctorEntity : BaseEntity
    {
        public string CRM { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public DoctorSpecialtyEnum Specialty { get; set; }

        public virtual IList<ScheduleDoctorEntity> SchedulesDoctor { get; set; }
    }
}
