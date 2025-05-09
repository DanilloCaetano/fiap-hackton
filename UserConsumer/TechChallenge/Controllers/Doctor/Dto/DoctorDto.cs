using Domain.Doctor.Model.Enum;

namespace RegistrationService.Controllers.Doctor.Dto
{
    public record DoctorDto
    {
        public string CRM { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public DateTime BirthDate { get; init; }
        public DoctorSpecialtyEnum Specialty { get; init; }
    }
}
