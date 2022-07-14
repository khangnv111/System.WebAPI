using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Models.ViewModel.User
{
    public class SaveGroupPermission
    {
        public int GroupId { get; set; }
        public List<PermissionConfig>? PermissionList { get; set; }

        //public int PermissionId { get; set; }
        //public bool IsView { get; set; }
        //public bool IsInsert { get; set; }
        //public bool IsUpdate { get; set; }
        //public bool IsDelete { get; set; }
    }

    public class PermissionConfig
    {
        public int PermissionId { get; set; }
        public bool IsView { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
}
