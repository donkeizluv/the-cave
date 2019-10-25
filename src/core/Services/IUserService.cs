using System.Collections.Generic;
using System.Threading.Tasks;
using CaveCore.DTO;
using CaveCore.Models;
using CaveCore.SchemaModels;

namespace CaveCore.Services
{
    public interface IUserService
    {
        Task<IUserValidateResult> Validate(UserDto user); 
        Task<IEnumerable<UserDto>> GetAll();
        Task Create(UserDto user);
    }
}