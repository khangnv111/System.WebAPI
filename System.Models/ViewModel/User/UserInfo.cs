using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Models.ViewModel.User
{
    public class UserInfo : BaseModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public int Status { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
    }

    public class UserPermission
    {
        public int PermissionId { get; set; }
        public int ParentId { get; set; }
        public string PermissionTitle { get; set; } = string.Empty;
        public string PermissionKey { get; set; } = string.Empty;
        public string PermissionUrl { get; set; } = string.Empty;
        public string RedirectUrl { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public int Position { get; set; }

        public bool IsView { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
}
