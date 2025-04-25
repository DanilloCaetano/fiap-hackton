using Domain.Patient.Model;

namespace Domain.Patient.Service
{
    public class PatientService : IPatientService
    {
        public Task AddPatient(PatientEntity patientEntity)
        {
            throw new NotImplementedException();
        }

        public Task<PatientEntity> GetPatientById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePatient(PatientEntity patientEntity)
        {
            throw new NotImplementedException();
        }
    }
}
