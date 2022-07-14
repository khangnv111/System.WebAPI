using System.Models.Model.Admin;

namespace System.EntityFramework.Responsitories.Users
{
    public interface IUserReponsitory
    {
        Task<User> AddUserAsync(User user, CancellationToken cancellationToken);
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByNameAsync(string userName);
        Task<List<User>> GetUserListAsync(string userName, int status = -1);
        Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken);
    }
}