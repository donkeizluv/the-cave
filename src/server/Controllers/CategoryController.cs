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
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService service, IPostService postService, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCates()
        {
            try
            {
                var cats = await _service.GetAllCates();
                return Ok(_mapper.Map<IEnumerable<CategoryDto>>(cats));
             }
            catch (BussinessException ex)
            {
                return BadRequest(ex.Message);
            }
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

        [HttpGet("cate/{cateId}")]
        public async Task<IActionResult> GetCateById(string cateId)
        {
            try
            {
                var cate =  await _service.GetCateById(cateId);
                if(cate != null){
                cate.PostCount = await _postService.CountPostByCate(cateId);
                }
                return Ok(cate);
            }
            catch (BussinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
