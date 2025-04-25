using Domain.Schedule.Model;
using Domain.Schedule.Repository;
using Infraestructure.Repository.Base;

namespace Infraestructure.Repository.Schedule
{
    public class ScheduleRepository : BaseRepository<ScheduleEntity>, IScheduleRepository
    {
        private readonly Context.TechChallengeContext _context;

        public ScheduleRepository(Context.TechChallengeContext context) : base(context)
        {
            _context = context;
        }
    }
}
