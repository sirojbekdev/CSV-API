using CsvTask.Models;

namespace CsvTask.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<List<User>> GetUsers(SortType sortType, int limit);
        Task<IEnumerable<User>> AddUsers(IEnumerable<User> users);
    }
}
