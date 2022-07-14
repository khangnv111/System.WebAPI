using System;
using System.Collections.Generic;
using System.EntityFramework.Data;
using System.Linq;
using System.Models.Model.Admin;
using System.Text;
using System.Threading.Tasks;

namespace System.EntityFramework.Responsitories.Users
{
    public class PermissionReponsitory : IPermissionReponsitory
    {
        private readonly AdminDbContext dbContext;
        public PermissionReponsitory(AdminDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region Permission
        public Task<List<Permission>> GetPermissionListAsync(int status = -1)
        {
            var list = dbContext.Permissions.Where(u => u.Status == status || status == -1).ToList();
            return Task.FromResult(list);
        }

        public Task<Permission?> GetPermissionByIdAsync(int permissionId)
        {
            var data = dbContext.Permissions.FirstOrDefault(u => u.PermissionId == permissionId);
            return Task.FromResult(data);
        }

        public Task<Permission> AddPermissionAsync(Permission permission, CancellationToken cancellationToken)
        {
            permission.Created = DateTime.Now;
            permission.Updated = DateTime.Now;
            _ = dbContext.Add(permission);
            _ = dbContext.SaveChanges();

            return Task.FromResult(permission);
        }

        public Task<Permission> UpdatePermissionAsync(Permission permission, CancellationToken cancellationToken)
        {
            permission.Updated = DateTime.Now;
            _ = dbContext.Update(permission);
            _ = dbContext.SaveChanges();

            return Task.FromResult(permission);
        }

        public Task DeletePermissionAsync(Permission permission, CancellationToken cancellationToken)
        {
            _ = dbContext.Remove(permission);
            _ = dbContext.SaveChanges();

            return Task.CompletedTask;
        }
        #endregion

        #region Group Permission
        public Task<List<GroupPermission>> GetGroupPermissionByIdAsync(int groupId)
        {
            var list = dbContext.GroupPermissions.Where(u => u.GroupId == groupId).ToList();
            return Task.FromResult(list);
        }

        public Task DeleteGroupPermissionAsync(int groupId, CancellationToken cancellationToken)
        {
            var list = dbContext.GroupPermissions.Where(u => u.GroupId == groupId).ToList();

            //dbContext.GroupPermissions.RemoveRange((IEnumerable<GroupPermission>)list);
            dbContext.RemoveRange(list);
            dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        public Task AddGroupPermissionAsync(List<GroupPermission> permission, CancellationToken cancellationToken)
        {
            _ = dbContext.AddRangeAsync(permission);
            _ = dbContext.SaveChanges();

            return Task.CompletedTask;
        }
        #endregion
    }
}
