using Common.Helpers;
using Common.MessagingService;
using Common.MessagingService.QueuesConfig;
using Domain.Doctor.Model;
using Domain.Patient.Model;
using Domain.Patient.Repository;
using Domain.User.Model.Dto;

namespace Domain.Patient.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IRabbitMqService _rabbitMqService;

        public PatientService(IPatientRepository patientRepository, IRabbitMqService rabbitMqService)
        {
            _patientRepository = patientRepository;
            _rabbitMqService = rabbitMqService;
        }

        public async Task AddPatient(PatientEntity patientEntity, string password)
        {
            await _patientRepository.AddAsync(patientEntity);

            var userDto = new UserDto
            {
                EntityId = patientEntity.Id,
                Credential = patientEntity.CPF,
                Name = patientEntity.FirstName,
                Type = User.Model.Enum.UserTypeEnum.Patient,
                Password = Encrypt.TEncrypt(password)
            };
            await _rabbitMqService.SendMessage(QueueNames.UserInsert, QueueNames.UserInsert, userDto);
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
