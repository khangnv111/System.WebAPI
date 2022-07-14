using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Models.Model.Admin
{
    public class Group : BaseModel
    {
        [Key]
        public int GroupId { get; set; }

        [MaxLength(250)]
        public string GroupName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public int Status { get; set; }
    }

    public enum EnumGroupStatus
    {
        NoActive,
        Active,
    }
}
