using Domain.Patient.Model;

namespace Domain.Patient.Service
{
    public interface IPatientService
    {
        Task<PatientEntity> GetPatientById(Guid id);
        Task AddPatient(PatientEntity patientEntity, string password);
        Task UpdatePatient(PatientEntity patientEntity);
    }
}
