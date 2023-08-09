using CsvTask.Models;

namespace CsvTask.Services
{
    public class DataSeedService : IDataSeedService
    {
        private readonly IUserService _userService;

        public DataSeedService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Seed()
        {
            var users = await _userService.GetAllUsers();

            if (!users.Any())
            {
                List<User> newUsers = new List<User>()
                {
                    new User()
                    {
                        Age = 20,
                        City = "Tashkent",
                        Username = "User1",
                        PhoneNumber = "135568",
                        Email = "random1@email.com"
                    },
                     new User()
                    {
                        Age = 30,
                        City = "Tashkent",
                        Username = "User2",
                        PhoneNumber = "827281",
                        Email = "random2@email.com"
                    }
                };

                await _userService.AddUsers(newUsers);
            }
        }
    }
}
