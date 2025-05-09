using Common.Helpers;
using Common.MessagingService;
using Common.MessagingService.QueuesConfig;
using Domain.Doctor.Model;
using Domain.Doctor.Model.Enum;
using Domain.Doctor.Repository;
using Domain.User.Model.Dto;

namespace Domain.Doctor.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IRabbitMqService _rabbitMqService;

        public DoctorService(IDoctorRepository doctorRepository, IRabbitMqService rabbitMqService)
        {
            _doctorRepository = doctorRepository;
            _rabbitMqService = rabbitMqService;
        }

        public async Task AddDoctor(DoctorEntity doctorEntity, string password)
        {
            await _doctorRepository.AddAsync(doctorEntity);

            var userDto = new UserDto
            {
                EntityId = doctorEntity.Id,
                Credential = doctorEntity.CRM,
                Name = doctorEntity.FirstName,
                Type = User.Model.Enum.UserTypeEnum.Doctor,
                Password = Encrypt.TEncrypt(password)
            };
            await _rabbitMqService.SendMessage(QueueNames.UserInsert, QueueNames.UserInsert, userDto);
        }

        public Task<IList<DoctorEntity>> GetDoctorByFilter(DoctorSpecialtyEnum? doctorSpecialty, DateTime? expectedDate)
        {
            return _doctorRepository.GetDoctorWithDesalocatedSchedules(doctorSpecialty, expectedDate);
        }
    }
}
