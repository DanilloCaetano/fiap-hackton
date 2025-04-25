using Domain.Schedule.Model;
using Domain.Schedule.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Schedule.Service
{
    public class ScheduleService : IScheduleService
    {
        public Task AddSchedule(ScheduleEntity scheduleEntity)
        {
            throw new NotImplementedException();
        }

        public Task CancelSchedule(Guid scheduleId, string justification)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ScheduleEntity>> GetSchedulesByDoctorId(Guid doctorId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ScheduleEntity>> GetSchedulesByDoctorId(Guid doctorId, ScheduleStatusEnum? scheduleStatusEnum)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSchedule(ScheduleEntity scheduleEntity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateScheduleStatus(Guid scheduleId, ScheduleStatusEnum newStatus)
        {
            throw new NotImplementedException();
        }
    }
}
