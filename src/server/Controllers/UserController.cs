using System.Threading.Tasks;
using AutoMapper;
using CaveCore.DTO;
using CaveCore.Exceptions;
using CaveCore.Models;
using CaveCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CaveServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IUserService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("validate")]
        public async Task<IActionResult> Validate([FromBody]ValidateUserDto user)
        {
            var result = await _service.Validate(_mapper.Map<UserDto>(user));
            return Ok(_mapper.Map<UserValidateResultDto>(result));
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

        [Authorize]
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok( _service.Ping());
        }
    }
}
