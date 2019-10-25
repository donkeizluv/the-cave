using System.Threading.Tasks;
using CaveCore.DTO;
using CaveCore.Exceptions;
using CaveCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CaveServer.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Validate([FromBody]UserDto user)
        {
            var result = await _service.Validate(user);
            if (result.Valid)
            {
                return Ok(result.Token);
            }
            return Forbid("Invalid username or passwords.");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]UserDto user)
        {
            try
            {
                await _service.Create(user);
                return Ok();
            }
            catch (BussinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
