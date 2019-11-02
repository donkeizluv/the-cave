using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using CaveCore.DTO;
using CaveCore.Exceptions;
using CaveCore.Models;
using CaveCore.SchemaModels;
using CaveCore.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using CaveCore.Service.Impl;

namespace CaveCore.Services.Impl
{
    public class UserService : ContextAwareService, IUserService
    {
        private readonly IMongoClient _client;
        private readonly IDbSettings _dbSettings;
        private readonly IAppSettings _appSettings;
        private readonly IMongoDatabase _db;
        private readonly IMapper _mapper;

        public UserService(IOptions<DbSettings> dbOption,
         IOptions<AppSettings> appOption, 
        IMapper mapper, 
        IMongoClient dbClient, 
        ClaimsPrincipal claimsPrincipal) : base(claimsPrincipal)
        {
            _dbSettings = dbOption.Value;
            _appSettings = appOption.Value;
            _client = dbClient;
            _db = _client.GetDatabase(_dbSettings.DatabaseName);
            _mapper = mapper;
        }

        public async Task Create(UserDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            var userCollection = _db.GetCollection<User>(_dbSettings.UserCollectionName);
            var exist = await userCollection
                                    .Find(u => u.Username == user.Username || u.Email == user.Email)
                                    .AnyAsync();
            if (exist)
            {
                throw new BussinessException("User with same infomation already existed");
            }
            var userModel = _mapper.Map<User>(user);
            userModel.Roles = new List<UserRoles>() { UserRoles.User };
            await _db.GetCollection<User>(_dbSettings.UserCollectionName)
                    .InsertOneAsync(userModel);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<UserDto>>(
                            await _db.GetCollection<User>(_dbSettings.UserCollectionName)
                                .Find(u => true)
                                .ToListAsync());
        }

        public async Task<IUserValidateResult> Validate(UserDto userDto)
        {
            if (userDto == null) throw new ArgumentNullException(nameof(userDto));
            var user = await _db.GetCollection<User>(_dbSettings.UserCollectionName)
                                    .Find(u => u.Username == userDto.Username && u.Pwd == userDto.Pwd)
                                    .FirstOrDefaultAsync();
            if (user == null)
            {
                // throw new BussinessException("Invalid user credentials");
                return new UserValidateResult()
                {
                    Valid = false,
                    Message = "Invalid username or password"
                };
            }
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = CreateIdentity(user),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new UserValidateResult()
            {
                Valid = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
        private ClaimsIdentity CreateIdentity(User user)
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in user.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
            }
            return identity;
        }

        public async Task<ValidateUserDto> Ping()
        {
             return await Task.FromResult(new ValidateUserDto(){
                Username = CurrentUsername,
                Id = CurrentId 
            });
           
        }
    }
}