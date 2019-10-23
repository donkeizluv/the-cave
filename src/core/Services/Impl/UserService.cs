using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CaveCore.DTO;
using CaveCore.Models;
using CaveCore.Settings;
using MongoDB.Driver;

namespace CaveCore.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoClient _client;
        private readonly IDbSettings _settings;
        private readonly IMongoDatabase _db;
        private readonly IMapper _mapper;

        public UserService(IDbSettings settings, IMapper mapper)
        {
            _settings = settings;
            _client = new MongoClient(settings.ConnectionString);
            _db = _client.GetDatabase(_settings.DatabaseName);
            _mapper = mapper;
        }

        public async Task Create(UserDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            await _db.GetCollection<User>(_settings.UserCollectionName)
                    .InsertOneAsync(_mapper.Map<User>(user));
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<UserDto>>(
                            await _db.GetCollection<User>(_settings.UserCollectionName)
                                .Find(u => true)
                                .ToListAsync());
        }

        public async Task<bool> Validate(UserDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            var foundUser = await _db.GetCollection<User>(_settings.UserCollectionName)
                                     .FindAsync(u => u.Username == user.Username && u.Pwd == user.Pwd);
            return await foundUser.AnyAsync();
        }

    }
}