using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Models.ViewModel.User
{
    public class SavePermission
    {
        public int ParentId { get; set; }
        public string PermissionTitle { get; set; } = string.Empty;

        public string PermissionKey { get; set; } = string.Empty;

        public string PermissionUrl { get; set; } = string.Empty;

        public string RedirectUrl { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;

        public int Position { get; set; }

        public int Status { get; set; }
    }
}
