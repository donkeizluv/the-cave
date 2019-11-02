using System.Collections.Generic;
using System.Threading.Tasks;
using CaveCore.DTO;
using CaveCore.SchemaModels;

namespace CaveCore.Services
{
    public interface ITrendingService
    {
        Task<bool> ReCalculatePoint();
        
    }
}