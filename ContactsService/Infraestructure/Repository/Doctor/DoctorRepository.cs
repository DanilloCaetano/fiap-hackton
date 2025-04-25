using Domain.Doctor.Model;
using Domain.Doctor.Repository;
using Infraestructure.Repository.Base;

namespace Infraestructure.Repository.Doctor
{
    public class DoctorRepository : BaseRepository<DoctorEntity>, IDoctorRepository
    {
        private readonly Context.TechChallengeContext _context;

        public DoctorRepository(Context.TechChallengeContext context) : base(context)
        {
            _context = context;
        }
    }
}
