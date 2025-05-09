using Domain.Doctor.Model;
using Domain.Doctor.Model.Enum;
using Domain.Doctor.Repository;
using Infraestructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infraestructure.Repository.Doctor
{
    public class DoctorRepository : BaseRepository<DoctorEntity>, IDoctorRepository
    {
        private readonly Context.TechChallengeContext _context;

        public DoctorRepository(Context.TechChallengeContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<DoctorEntity>> GetDoctorWithDesalocatedSchedules(DoctorSpecialtyEnum? doctorSpecialty, DateTime? expectedDate)
        {
            Expression<Func<DoctorEntity, bool>> filter = d => true &&
                doctorSpecialty.HasValue ? d.Specialty == doctorSpecialty : true;

            return await _context.Doctors
                .Include(x => x.SchedulesDoctor
                        .Where(s => !s.IsAllocated && (expectedDate.HasValue ? s.DateHour.Date == expectedDate.Value.Date : true)))
                    .Where(filter)
                .ToListAsync();
        }
    }
}
