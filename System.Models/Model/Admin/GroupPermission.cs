using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Models.Model.Admin
{
    public class GroupPermission
    {
        [Key]
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int PermissionId { get; set; }
        public bool IsView { get; set; }
        public bool IsInsert { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
}
