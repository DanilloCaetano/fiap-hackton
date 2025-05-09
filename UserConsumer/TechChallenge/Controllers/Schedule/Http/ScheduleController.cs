using AutoMapper;
using Common.MessagingService;
using Domain.Schedule.Model;
using Domain.Schedule.Model.Enum;
using Domain.Schedule.Service;
using Integration;
using Microsoft.AspNetCore.Mvc;
using RegistrationService.Controllers.Schedule.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace TechChallenge1.Controllers.Contacts.Http
{
    [Route("api/[controller]")]
    [SwaggerTag("Endpoints to manage schedules")]
    [Produces("application/json")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IIntegrationService _integrationService;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IMapper _mapper;

        public ScheduleController(IScheduleService scheduleService,
            IRabbitMqService rabbitMqService,
            IIntegrationService integrationService,
            IMapper mapper)
        {
            _rabbitMqService = rabbitMqService;
            _integrationService = integrationService;
            _scheduleService = scheduleService;
            _mapper = mapper;
        }

        /// <summary>
        /// Add a new schedule
        /// </summary>
        /// <param name="dto">Schedule DTO</param>
        /// <response code="201">Schedule created with success</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddSchedule([FromBody] ScheduleDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);

                var scheduleEntity = _mapper.Map<ScheduleEntity>(dto);
                await _scheduleService.AddSchedule(scheduleEntity);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Cancel a schedule
        /// </summary>
        /// <param name="justification"></param>
        /// <param name="scheduleId">Schedule Id</param>
        /// <response code="204">Schedule cancelled with success</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("cancel/{scheduleId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CancelSchedule([FromBody] JustificationDto justification, [FromRoute] Guid scheduleId)
        {
            try
            {
                await _scheduleService.CancelSchedule(scheduleId, justification.Justification);
                return StatusCode(StatusCodes.Status204NoContent);
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Update status of a schedule
        /// </summary>
        /// <param name="newStatus"></param>
        /// <param name="scheduleId">Schedule Id</param>
        /// <response code="204">Schedule update with success</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("change-status/{scheduleId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateStatusSchedule([FromBody] StatusDto newStatus, [FromRoute] Guid scheduleId)
        {
            try
            {
                await _scheduleService.UpdateScheduleStatus(scheduleId, newStatus.Status);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Get Schedule by Doctor Id
        /// </summary>
        /// <response code="200">Schedule list</response>
        /// <response code="400">Bad Request</response>
        [HttpGet("by-doctor/{doctorId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetScheduleByDoctorId([FromRoute] Guid doctorId, [FromQuery] ScheduleStatusEnum statusFilter)
        {
            try
            {
                var schedules = await _scheduleService.GetSchedulesByDoctorId(doctorId, statusFilter);
                return StatusCode(StatusCodes.Status200OK, schedules);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
