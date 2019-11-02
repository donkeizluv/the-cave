using System.Collections.Generic;
using System.Threading.Tasks;
using CaveCore.DTO;
using CaveCore.SchemaModels;

namespace CaveCore.Services
{
    public interface ICategoryService
    {
        Task<string> Create(CategoryDto category);
        Task<IEnumerable<ICategory>> GetAllCates();
        Task<CategoryDto> GetCateById(string cateId);

        Task<string> GetCateNameById(string cateId);
    }
}