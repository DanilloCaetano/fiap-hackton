namespace RegistrationService.Controllers.ScheduleDoctor.Dto
{
    public class ScheduleDoctorDto
    {
        public DateTime DateHour { get; set; }
        public bool IsAllocated { get; set; }
        public Guid DoctorId { get; set; }
    }
}
