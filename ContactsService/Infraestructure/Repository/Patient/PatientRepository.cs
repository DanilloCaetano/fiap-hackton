using Domain.Patient.Model;
using Domain.Patient.Repository;
using Infraestructure.Repository.Base;

namespace Infraestructure.Repository.Patient
{
    public class PatientRepository : BaseRepository<PatientEntity>, IPatientRepository
    {
        private readonly Context.TechChallengeContext _context;

        public PatientRepository(Context.TechChallengeContext context) : base(context)
        {
            _context = context;
        }
    }
}
