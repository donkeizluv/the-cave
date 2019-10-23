using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaveCore.DTO;
using CaveCore.Service;
using CaveCore.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cave_server.Controllers
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
        public async Task<bool> Validate([FromBody]UserDto user)
        {
            return await _service.Validate(user);
        }

        [HttpPost]
        public async Task Create([FromBody]UserDto user)
        {
            await _service.Create(user);
        }
    }
}
