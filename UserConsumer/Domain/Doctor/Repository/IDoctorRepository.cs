using Domain.Base.Repository;
using Domain.Doctor.Model;
using Domain.Doctor.Model.Enum;

namespace Domain.Doctor.Repository
{
    public interface IDoctorRepository : IBaseRepository<DoctorEntity>
    {
        Task<IList<DoctorEntity>> GetDoctorWithDesalocatedSchedules(DoctorSpecialtyEnum? doctorSpecialty, DateTime? expectedDate);
    }
}
