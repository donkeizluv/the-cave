using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CaveCore.DTO;
using CaveCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CaveCore.Exceptions;

namespace CaveServer.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class PostController : ControllerBase
    {

        private readonly IPostService _service;
        private readonly ILogger<PostController> _logger;
        private readonly IMapper _mapper;

        public PostController(ILogger<PostController> logger, IPostService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<PostDto>> GetAllPostWithCateId(string cateId)
        {
            var posts = await _service.GetAllPostByCateId(cateId);
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<PostDto>> GetAllPost()
        {

                var posts = await _service.GetAllPost();
                return _mapper.Map<IEnumerable<PostDto>>(posts);       
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<PostDto> GetPostById(string postId)
        {
            var post = await _service.GetPostById(postId);
            return _mapper.Map<PostDto>(post);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PostDto post)
        {
            try{
                return Ok(await _service.Create(post));
            }catch  (BussinessException ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(string postId)
        {
            try{
                return Ok(await _service.DeletePost(postId));
            }catch(BussinessException ex){
                return BadRequest(ex.Message);
            }
            
        }

    }
}
