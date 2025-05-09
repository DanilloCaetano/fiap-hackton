using Domain.Schedule.Model;
using Domain.Schedule.Model.Enum;

namespace Domain.Schedule.Service
{
    public interface IScheduleService
    {
        Task<IList<ScheduleEntity>> GetSchedulesByDoctorId(Guid doctorId, ScheduleStatusEnum? scheduleStatusEnum);
        Task AddSchedule(ScheduleEntity scheduleEntity);
        //Task UpdateSchedule(ScheduleEntity scheduleEntity);
        Task UpdateScheduleStatus(Guid scheduleId, ScheduleStatusEnum newStatus);
        Task CancelSchedule(Guid scheduleId, string justification);
    }
}
