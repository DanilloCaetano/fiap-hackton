using AutoMapper;
using Common.MessagingService;
using Common.MessagingService.QueuesConfig;
using Domain.Doctor.Model;
using Domain.Doctor.Model.Enum;
using Domain.Doctor.Service;
using Microsoft.AspNetCore.Mvc;
using RegistrationService.Controllers.Doctor.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace RegistrationService.Controllers.Doctor.Http
{
    [Route("api/[controller]")]
    [SwaggerTag("Endpoints to manage doctors")]
    [Produces("application/json")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;
        private readonly IRabbitMqService _rabbitMqService;

        public DoctorController(IDoctorService doctorService, IMapper mapper, IRabbitMqService rabbitMqService)
        {
            _doctorService = doctorService;
            _mapper = mapper;
            _rabbitMqService = rabbitMqService;
        }

        /// <summary>
        /// Add a new doctor
        /// </summary>
        /// <param name="dto">doctor DTO</param>
        /// <response code="201">Doctor created with success</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddDoctor([FromBody] DoctorDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);

                var doctorEntity = _mapper.Map<DoctorEntity>(dto);
                await _doctorService.AddDoctor(doctorEntity, dto.Password);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Get doctors
        /// </summary>
        /// <response code="200">List of doctors</response>
        /// <response code="400">Bad Request</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDoctorsByFilter([FromQuery] DoctorSpecialtyEnum? doctorSpecialty, [FromQuery] DateTime? expectedDate)
        {
            try
            {
                var doctors = await _doctorService.GetDoctorByFilter(doctorSpecialty, expectedDate);

                return StatusCode(StatusCodes.Status200OK, doctors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
