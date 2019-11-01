using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CaveCore.DTO;
using CaveCore.Exceptions;
using CaveCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CaveServer.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _service;
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> GetCates()
        {
            var cats = await _service.GetAllCates();
            return _mapper.Map<IEnumerable<CategoryDto>>(cats);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CategoryDto cat)
        {
            try
            {
                return Ok(await _service.Create(cat));
            }
            catch (BussinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
