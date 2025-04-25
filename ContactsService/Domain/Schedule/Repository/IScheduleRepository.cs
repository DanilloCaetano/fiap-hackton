using Domain.Base.Repository;
using Domain.Contact.Entity;
using Domain.Schedule.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Schedule.Repository
{
    public interface IScheduleRepository : IBaseRepository<ScheduleEntity>
    {
    }
}
