using Domain.Patient.Model;
using Domain.Patient.Repository;

namespace Domain.Patient.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task AddPatient(PatientEntity patientEntity)
        {
            await _patientRepository.AddAsync(patientEntity);
        }

        public async Task<PatientEntity> GetPatientById(Guid id)
        {
            return await _patientRepository.GetByIdAsync(id);
        }

        public async Task UpdatePatient(PatientEntity patientEntity)
        {
            await _patientRepository.UpdateAsync(patientEntity);
        }
    }
}
