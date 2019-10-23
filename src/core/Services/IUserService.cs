using System.Collections.Generic;
using System.Threading.Tasks;
using CaveCore.DTO;
using CaveCore.Models;

namespace CaveCore.Services
{
    public interface IUserService
    {
        Task<bool> Validate(UserDto user); 
        Task<IEnumerable<UserDto>> GetAll();
        Task Create(UserDto user);
    }
}