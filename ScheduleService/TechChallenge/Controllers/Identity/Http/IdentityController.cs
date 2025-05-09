using Domain.Patient.Model;
using Domain.User.Service;
using Microsoft.AspNetCore.Mvc;
using RegistrationService.Controllers.Identity.Dto;
using RegistrationService.Controllers.Patient.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace RegistrationService.Controllers.Identity.Http
{
    [Route("api/[controller]")]
    [SwaggerTag("Endpoints to manage identities")]
    [Produces("application/json")]
    public class IdentityController : Controller
    {

        private IUserService _userService;

        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Do Login
        /// </summary>
        /// <param name="dto">Login DTO</param>
        /// <response code="201">patient created with success</response>
        /// <response code="400">Bad Request</response>
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);

                var tkn = await _userService.Login(dto.Credential, dto.Password);

                return StatusCode(StatusCodes.Status200OK, tkn);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
