using System.Collections.Generic;
using System.Threading.Tasks;
using CaveCore.DTO;
using CaveCore.SchemaModels;

namespace CaveCore.Services
{
    public interface IPostService
    {
        Task<string> Create(PostDto post);

        Task<IEnumerable<IPost>> GetAllPostByCateId(string cateId);

        Task<IEnumerable<IPost>> GetAllPost();
        
        Task<IPost> GetPostById(string postId);

        Task<string> DeletePost(string postId);
    }
}