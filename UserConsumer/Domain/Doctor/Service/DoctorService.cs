using Domain.Doctor.Model;
using Domain.Doctor.Model.Enum;
using Domain.Doctor.Repository;

namespace Domain.Doctor.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task AddDoctor(DoctorEntity doctorEntity)
        {
            await _doctorRepository.AddAsync(doctorEntity);
        }

        public Task<IList<DoctorEntity>> GetDoctorByFilter(DoctorSpecialtyEnum? doctorSpecialty, DateTime? expectedDate)
        {
            return _doctorRepository.GetDoctorWithDesalocatedSchedules(doctorSpecialty, expectedDate);
        }
    }
}
