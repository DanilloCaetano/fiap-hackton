using Domain.Doctor.Model;
using Domain.Doctor.Repository;
using Infraestructure.Repository.Base;

namespace Infraestructure.Repository.Doctor
{
    public class ScheduleDoctorRepository : BaseRepository<ScheduleDoctorEntity>, IScheduleDoctorRepository
    {
        private readonly Context.TechChallengeContext _context;

        public ScheduleDoctorRepository(Context.TechChallengeContext context) : base(context)
        {
            _context = context;
        }
    }
}
