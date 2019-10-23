using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CaveCore.DTO;
using CaveCore.Models;
using CaveCore.Service;
using CaveCore.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace cave_server.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
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
        public async Task<IEnumerable<CategoryDto>> GetCat()
        {
            var cats = await _service.GetAll();
            return _mapper.Map<IEnumerable<CategoryDto>>(cats);
        }

        [HttpPost]
        public async Task Create([FromBody]CategoryDto cat)
        {
            await _service.Create(cat);
        }
    }
}
