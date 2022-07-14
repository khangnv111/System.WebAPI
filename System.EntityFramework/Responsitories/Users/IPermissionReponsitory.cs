using System.Models.Model.Admin;

namespace System.EntityFramework.Responsitories.Users
{
    public interface IPermissionReponsitory
    {
        Task AddGroupPermissionAsync(List<GroupPermission> permission, CancellationToken cancellationToken);
        Task<Permission> AddPermissionAsync(Permission permission, CancellationToken cancellationToken);
        Task DeleteGroupPermissionAsync(int groupId, CancellationToken cancellationToken);
        Task DeletePermissionAsync(Permission permission, CancellationToken cancellationToken);
        Task<List<GroupPermission>> GetGroupPermissionByIdAsync(int groupId);
        Task<Permission?> GetPermissionByIdAsync(int permissionId);
        Task<List<Permission>> GetPermissionListAsync(int status = -1);
        Task<Permission> UpdatePermissionAsync(Permission permission, CancellationToken cancellationToken);
    }
}