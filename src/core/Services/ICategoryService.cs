using System.Collections.Generic;
using System.Threading.Tasks;
using CaveCore.DTO;
using CaveCore.Models;

namespace CaveCore.Services
{
    public interface ICategoryService
    {
        Task Create(CategoryDto category);
        Task<IEnumerable<ICategory>> GetAll();
    }
}