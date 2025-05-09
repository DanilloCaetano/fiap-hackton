using Domain.Base.Repository;
using Domain.Schedule.Model;
using Domain.Schedule.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Schedule.Repository
{
    public interface IScheduleRepository : IBaseRepository<ScheduleEntity>
    {
        Task<IList<ScheduleEntity>> GetSchedulesByDoctorId(Guid doctorId, ScheduleStatusEnum? scheduleStatusEnum);
    }
}
