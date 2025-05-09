using Domain.Schedule.Model.Enum;

namespace RegistrationService.Controllers.Schedule.Dto
{
    public record StatusDto
    {
        public ScheduleStatusEnum Status { get; init; }
    }
}
