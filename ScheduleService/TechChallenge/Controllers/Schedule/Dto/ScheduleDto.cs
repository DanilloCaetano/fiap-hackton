using Domain.Schedule.Model.Enum;

namespace RegistrationService.Controllers.Schedule.Dto
{
    public class ScheduleDto
    {
        public Guid PatientId { get; set; }
        public Guid ScheduleDoctorId { get; set; }
        public DateTime DateHour { get; set; }
        public ScheduleStatusEnum Status { get; set; } = ScheduleStatusEnum.Pending;
        public string? CancellationJustification { get; set; }
    }
}
