using System.Threading.Tasks;
using AutoMapper;
using CaveCore.DTO;
using CaveCore.Exceptions;
using CaveCore.Models;
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
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IUserService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Validate([FromBody]UserDto user)
        {
            var result = await _service.Validate(user);
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
    }
}
