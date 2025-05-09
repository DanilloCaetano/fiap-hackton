namespace RegistrationService.Controllers.Patient.Dto
{
    public class PatientDto
    {
        public string CPF { get; set; }
        public DateTime BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
