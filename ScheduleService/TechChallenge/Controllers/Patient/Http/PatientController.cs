using AutoMapper;
using Domain.Patient.Model;
using Domain.Patient.Service;
using Domain.Schedule.Model;
using Microsoft.AspNetCore.Mvc;
using RegistrationService.Controllers.Patient.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace RegistrationService.Controllers.Patient.Http
{
    [Route("api/[controller]")]
    [SwaggerTag("Endpoints to manage schedules")]
    [Produces("application/json")]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public PatientController(IPatientService patientService, IMapper mapper)
        {
            _mapper = mapper;
            _patientService = patientService;
        }

        /// <summary>
        /// Add a new patient
        /// </summary>
        /// <param name="dto">patient DTO</param>
        /// <response code="201">patient created with success</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddPatient([FromBody] PatientDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);

                var patientEntity = _mapper.Map<PatientEntity>(dto);
                await _patientService.AddPatient(patientEntity, dto.Password);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
