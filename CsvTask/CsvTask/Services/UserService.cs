using CsvTask.Data;
using CsvTask.Models;
using MongoDB.Driver;

namespace CsvTask.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoClient _client;
        private IMongoCollection<User> _users;

        public UserService(IMongoClient client, IMongoDbSetting dbSetting)
        {
            _client = client;
            var database = _client.GetDatabase(dbSetting.DatabaseName);
            _users = database.GetCollection<User>(nameof(User));
            _client = client;
        }

        public async Task<IEnumerable<User>> AddUsers(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                var filter = Builders<User>.Filter.Eq(doc => doc.Id, user.Id);
                var result = await _users.Find(filter).ToListAsync();
                if (!result.Any())
                {
                    await _users.InsertOneAsync(user);
                }
                else
                {
                    await _users.ReplaceOneAsync(filter, user);
                }
            }

            return users;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _users.Find(_ => true).ToListAsync();

            return users;
        }

        public async Task<List<User>> GetUsers(SortType sortType, int limit)
        {
            var sort = sortType switch
            {
                SortType.ASC => Builders<User>.Sort.Ascending(x => x.Username),
                SortType.DESC => Builders<User>.Sort.Descending(x => x.Username),
                _ => Builders<User>.Sort.Ascending(x => x.Username)
            };

            var users = await _users.Find(_ => true).Sort(sort).Limit(limit).ToListAsync();

            return users;
        }
    }
}
