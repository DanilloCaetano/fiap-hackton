using AutoMapper;
using Domain.Doctor.Model;
using Domain.Patient.Model;
using Domain.Schedule.Model;
using RegistrationService.Controllers.Doctor.Dto;
using RegistrationService.Controllers.Patient.Dto;
using RegistrationService.Controllers.Schedule.Dto;
using RegistrationService.Controllers.ScheduleDoctor.Dto;

namespace RegistrationService.DomainInjection
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ScheduleEntity, ScheduleDto>().ReverseMap();
            CreateMap<ScheduleDoctorEntity, ScheduleDoctorDto>().ReverseMap();
            CreateMap<DoctorEntity, DoctorDto>().ReverseMap();
            CreateMap<PatientEntity, PatientDto>().ReverseMap();
        }
    }
}
