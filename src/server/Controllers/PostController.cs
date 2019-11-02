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
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;
        private readonly ILogger<PostController> _logger;
        private readonly IMapper _mapper;

        public PostController(ILogger<PostController> logger,
                            IPostService service,
                            IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{cateId}")]
        [AllowAnonymous]
        public async Task<IEnumerable<PostListDto>> GetPostsByCateId([FromQuery]string cateId, int? order)
        {
            var posts = await _service.GetPostsByCateId(cateId, order);
            return _mapper.Map<IEnumerable<PostListDto>>(posts);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<PostListDto>> GetAllPost(int? order)
        {

            var posts = await _service.GetAllPosts(order);
            return _mapper.Map<IEnumerable<PostListDto>>(posts);
        }

        [HttpGet("{postId}")]
        [AllowAnonymous]
        public async Task<PostDto> GetPostById([FromQuery]string postId)
        {
            var post = await _service.GetPostById(postId);
            return _mapper.Map<PostDto>(post);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]PostDto post)
        {
            try
            {
                return Ok(await _service.Create(post));
            }
            catch (BussinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody]PostDto post)
        {
            try
            {
                return Ok(await _service.Update(post));
            }
            catch (BussinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> Delete([FromQuery]string postId)
        {
            try
            {
                return Ok(await _service.DeletePost(postId));
            }
            catch (BussinessException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IEnumerable<PostListDto>> GetPostsByTilte([FromQuery]string cateId, [FromQuery]string searchText)
        {
            var posts = await _service.SearchPostWithCateId(cateId, searchText);
            return _mapper.Map<IEnumerable<PostListDto>>(posts);
        }
        [HttpPost("addvote")]
        public async Task<IActionResult> AddVote([FromBody]VoteRequestDto voteReq)
        {
            try
            {
                var post = await _service.AddVote(voteReq);
                return Ok(_mapper.Map<PostDto>(post));
            }
            catch (BussinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("addcomment")]
        public async Task<IActionResult> AddComment([FromBody]CommentDto comment)
        {
            try
            {
                var id = await _service.AddComment(comment);
                return Ok(id);
            }
            catch (BussinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("updatecomment")]
        public async Task<IActionResult> UpdateComment([FromBody]CommentDto comment)
        {
            try
            {
                var id = await _service.UpdateComment(comment);
                return Ok(id);
            }
            catch (BussinessException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
