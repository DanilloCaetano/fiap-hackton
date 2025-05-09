using Domain.Schedule.Model;
using Domain.Schedule.Model.Enum;
using Domain.Schedule.Repository;
using Infraestructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Infraestructure.Repository.Schedule
{
    public class ScheduleRepository : BaseRepository<ScheduleEntity>, IScheduleRepository
    {
        private readonly Context.TechChallengeContext _context;

        public ScheduleRepository(Context.TechChallengeContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<ScheduleEntity>> GetSchedulesByDoctorId(Guid doctorId, ScheduleStatusEnum? scheduleStatusEnum)
        {
            var query = _context.Schedules
                .Include(s => s.ScheduleDoctor)
                    .Where(s => s.ScheduleDoctor.DoctorId == doctorId)
                .AsQueryable();
            
            if (scheduleStatusEnum.HasValue)
            {
                query = query.Where(s => s.Status == scheduleStatusEnum.Value);
            }

            return await query.ToListAsync();
        }
    }
}
