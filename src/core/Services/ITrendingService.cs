using System.Threading.Tasks;

namespace CaveCore.Services
{
    public interface ITrendingService
    {
        Task<bool> ReCalculatePoint();
        
    }
}