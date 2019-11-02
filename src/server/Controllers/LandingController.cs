using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CaveCore.DTO;
using CaveCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CaveServer.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class LandingController : ControllerBase
    {

        private readonly IPostService _postService;
        private readonly ICategoryService _cateService;
        private readonly ILogger<LandingController> _logger;
        private readonly IMapper _mapper;

        public LandingController(ILogger<LandingController> logger, 
            IPostService postService, 
            ICategoryService cateService, 
            IMapper mapper)
        {
            _logger = logger;
            _postService = postService;
            _cateService = cateService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<LandingDto> GetLanding([FromQuery]int? order)
        {
            var posts = await _postService.GetAllPosts(order);
            var cates = await _cateService.GetAllCates();
            var postAndCate = new LandingDto();
            postAndCate.TrendingPosts = _mapper.Map<IEnumerable<PostListDto>>(posts);
            postAndCate.Categories = _mapper.Map<IEnumerable<CategoryDto>>(cates);
            return postAndCate;
        }

    }
}
