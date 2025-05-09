namespace RegistrationService.Controllers.Identity.Dto
{
    public record LoginDto
    {
        public string Credential { get; set; }
        public string Password { get; set; }
    }
}
