using Domain.Base.Repository;
using Domain.Doctor.Model;

namespace Domain.Doctor.Repository
{
    public interface IDoctorRepository : IBaseRepository<DoctorEntity>
    {
    }
}
