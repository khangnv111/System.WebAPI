using System;
using System.Collections.Generic;
using System.EntityFramework.Data;
using System.Linq;
using System.Models.Model.Admin;
using System.Text;
using System.Threading.Tasks;

namespace System.EntityFramework.Responsitories.Users
{
    public class GroupReponsitory : IGroupReponsitory
    {
        private readonly AdminDbContext dbContext;
        public GroupReponsitory(AdminDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<Group>> GetGroupListAsync(int status = -1)
        {
            var list = dbContext.Groups.Where(u => u.Status == status || status == -1).OrderByDescending(u => u.GroupName).ToList();
            return Task.FromResult(list);
        }

        public Task<Group?> GetGroupByIdAsync(int groupId)
        {
            var group = dbContext.Groups.FirstOrDefault(u => u.GroupId == groupId);
            return Task.FromResult(group);
        }

        public Task<Group> AddGroupAsync(Group group)
        {
            group.Created = DateTime.Now;
            group.Updated = DateTime.Now;
            _ = dbContext.Add(group);
            _ = dbContext.SaveChanges();

            return Task.FromResult(group);
        }

        public Task<Group> UpdateGroupAsync(Group group)
        {
            group.Updated = DateTime.Now;
            _ = dbContext.Update(group);
            _ = dbContext.SaveChanges();

            return Task.FromResult(group);
        }
    }
}
