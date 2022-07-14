using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Models;
using System.Models.Model;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace System.Utils.Auth
{
    public class JwtTokenServices
    {
        private IHttpContextAccessor httpContextAccessor;
        private readonly AuthenToken jwtToken;
        public JwtTokenServices(IOptions<AuthenToken> options, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.jwtToken = options.Value;
        }

        public HttpContext Current => this.httpContextAccessor.HttpContext;

        public string GetJwtTokenString(JwtTokenDataModel data)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(jwtToken.Key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("AccountID", data.UserID.ToString()),
                        new Claim("UserName", data.UserName.ToString()),
                        new Claim("GroupID", data.GroupID.ToString()),
                        new Claim("Email", data.Email.ToString()),
                    }),
                    Expires = DateTime.Now.AddHours(jwtToken.TimeExpire),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int UserId
        {
            get
            {
                try
                {
                    int userId = 0;
                    if (Current.User.Identity.IsAuthenticated)
                    {
                        string val = Current.User.FindFirst("AccountID").Value;
                        userId = Int32.Parse(val);
                    }
                    return userId;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public string UserName
        {
            get
            {
                string val = "";
                try
                {
                    if (Current.User.Identity.IsAuthenticated)
                    {
                        val = Current.User.FindFirst("UserName").Value;
                    }
                    return val;
                }
                catch
                {
                    return val;
                }
            }
        }

        public int GroupID
        {
            get
            {
                string val = "";
                try
                {
                    if (Current.User.Identity.IsAuthenticated)
                    {
                        val = Current.User.FindFirst("GroupID").Value;
                    }
                    return Convert.ToInt32(val);
                }
                catch
                {
                    return 0;
                }
            }
        }

        public string Email
        {
            get
            {
                string val = "";
                try
                {
                    if (Current.User.Identity.IsAuthenticated)
                    {
                        val = Current.User.FindFirst("Email").Value;
                    }
                    return val;
                }
                catch
                {
                    return val;
                }
            }
        }
    }
}
