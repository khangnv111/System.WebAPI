using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Models.Model.Admin
{
    public class User : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Password { get; set; } = string.Empty;

        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(250)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Avatar { get; set; } = string.Empty;

        public int Status { get; set; }

        public int GroupId { get; set; }
    }
    public enum UserStatus
    {
        NonActive,
        Active,
    }
}
