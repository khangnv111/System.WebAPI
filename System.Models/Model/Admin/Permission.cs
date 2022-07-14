using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Models.Model.Admin
{
    public class Permission : BaseModel
    {
        [Key]
        public int PermissionId { get; set; }

        public int ParentId { get; set; }

        [MaxLength(500)]
        public string PermissionTitle { get; set; } = string.Empty;

        [MaxLength(500)]
        public string PermissionKey { get; set; } = string.Empty;

        [MaxLength(500)]
        public string PermissionUrl { get; set; } = string.Empty;

        [MaxLength(500)]
        public string RedirectUrl { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Icon { get; set; } = string.Empty;

        public int Position { get; set; }

        public int Status { get; set; }
    }
}
