using Domain.Doctor.Model;
using Domain.Doctor.Model.Enum;

namespace Domain.Doctor.Service
{
    public class DoctorService : IDoctorService
    {
        public Task AddDoctor(DoctorEntity doctorEntity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<DoctorEntity>> GetDoctorByFilter(DoctorSpecialtyEnum? doctorSpecialty, DateTime? expectedDate)
        {
            throw new NotImplementedException();
        }
    }
}
