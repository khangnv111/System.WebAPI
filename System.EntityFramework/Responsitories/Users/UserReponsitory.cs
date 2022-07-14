using System;
using System.Collections.Generic;
using System.EntityFramework.Data;
using System.Linq;
using System.Models.Model.Admin;
using System.Text;
using System.Threading.Tasks;

namespace System.EntityFramework.Responsitories.Users
{
    public class UserReponsitory : IUserReponsitory
    {
        private readonly AdminDbContext dbContext;
        public UserReponsitory(AdminDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<User>> GetUserListAsync(string userName, int status = -1)
        {
            var list = dbContext.Users.Where(u => u.UserName.StartsWith(userName))
                .Where(u => u.Status == status || status == -1).OrderByDescending(u => u.Created).ToList();
            return Task.FromResult(list);
        }

        public Task<User?> GetUserByNameAsync(string userName)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.UserName == userName);
            return Task.FromResult(user);
        }

        public Task<User?> GetUserByIdAsync(int userId)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            return Task.FromResult(user);
        }

        public Task<User> AddUserAsync(User user, CancellationToken cancellationToken)
        {
            user.Created = DateTime.Now;
            user.Updated = DateTime.Now;
            _ = dbContext.Add(user);
            _ = dbContext.SaveChanges();

            return Task.FromResult(user);
        }

        public Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken)
        {
            user.Updated = DateTime.Now;
            _ = dbContext.Update(user);
            _ = dbContext.SaveChanges();

            return Task.FromResult(user);
        }
    }
}
