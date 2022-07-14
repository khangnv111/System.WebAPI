using System.Models.Model.Admin;

namespace System.EntityFramework.Responsitories.Users
{
    public interface IGroupReponsitory
    {
        Task<Group> AddGroupAsync(Group group);
        Task<Group?> GetGroupByIdAsync(int groupId);
        Task<List<Group>> GetGroupListAsync(int status = -1);
        Task<Group> UpdateGroupAsync(Group group);
    }
}