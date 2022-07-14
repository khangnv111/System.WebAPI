using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Models.Model
{
    public class AuthenToken
    {
        public string Key { get; set; } = string.Empty;
        public int TimeExpire { get; set; }
    }

    public class JwtTokenDataModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int GroupID { get; set; }
    }
}
