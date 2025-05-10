using AutoMapper;
using Domain.Doctor.Model;
using Domain.Doctor.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistrationService.Controllers.Doctor.Dto;
using RegistrationService.Controllers.ScheduleDoctor.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace RegistrationService.Controllers.ScheduleDoctor.Http
{
    [Route("api/schedule-doctor")]
    [SwaggerTag("Endpoints to manage schedule doctors")]
    [Produces("application/json")]
    [Authorize]
    public class ScheduleDoctorController : Controller
    {
        private readonly IScheduleDoctorService _scheduleDoctorService;
        private readonly IMapper _mapper;

        public ScheduleDoctorController(IScheduleDoctorService scheduleDoctorService, IMapper mapper)
        {
            _scheduleDoctorService = scheduleDoctorService;
            _mapper = mapper;
        }

        /// <summary>
        /// Add a new schedule doctor
        /// </summary>
        /// <param name="dto">schedule doctor DTO</param>
        /// <response code="201">Schedule Doctor created with success</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddScheduleDoctor([FromBody] ScheduleDoctorDto dto)
        {
            try
            {
                //GET ID BY TOKEN
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);

                var scheduleEntity = _mapper.Map<ScheduleDoctorEntity>(dto);
                await _scheduleDoctorService.AddSchedule(scheduleEntity);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Update schedule doctor
        /// </summary>
        /// <param name="dto">schedule doctor DTO</param>
        /// <response code="201">Schedule Doctor created with success</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("{scheduleDoctorId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateScheduleDoctor([FromBody] ScheduleDoctorDto dto, [FromRoute] Guid scheduleDoctorId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);

                //GET ID BY TOKEN
                var scheduleEntity = _mapper.Map<ScheduleDoctorEntity>(dto);
                var tknDoctorId = scheduleEntity.DoctorId;
                scheduleEntity.Id = scheduleDoctorId;
                await _scheduleDoctorService.UpdateSchedule(scheduleDoctorId, scheduleEntity, tknDoctorId);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

    }
}
