using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.EntityFramework.Responsitories.Users;
using System.Linq;
using System.Models.Model;
using System.Models.ViewModel;
using System.Text;
using System.Threading.Tasks;
using System.Utils;
using System.Utils.Auth;

namespace System.EntityFramework.Commands.Auth
{
    public class CmsLoginCommand
    {
        private readonly IGroupReponsitory groupReponsitory;
        private readonly IUserReponsitory userReponsitory;
        private readonly JwtTokenServices jwtToken;

        public CmsLoginCommand(IGroupReponsitory groupReponsitory, IUserReponsitory userReponsitory, JwtTokenServices jwtToken)
        {
            this.groupReponsitory = groupReponsitory;
            this.userReponsitory = userReponsitory;
            this.jwtToken = jwtToken;
        }

        public async Task<IActionResult> ExecuteAsync(AuthLogin auth, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(auth.UserName) || string.IsNullOrEmpty(auth.Password))
                {
                    return new OkObjectResult(new DataResponse("Input is invalid"));
                }

                // get user
                var user = await userReponsitory.GetUserByNameAsync(auth.UserName).ConfigureAwait(false);
                if (user == null)
                {
                    return new OkObjectResult(new DataResponse("Tài khoản không tồn tại"));
                }

                // check password
                var password = Common.MD5String(auth.Password);
                if (password != user.Password)
                {
                    return new OkObjectResult(new DataResponse("Mật khẩu không đúng"));
                }

                // check status
                if (user.Status == 0)
                {
                    return new OkObjectResult(new DataResponse("Tài khoản đã bị khóa"));
                }

                // check group
                var groupId = user.GroupId;
                var groupUser = await groupReponsitory.GetGroupByIdAsync(groupId).ConfigureAwait(false);
                if (groupUser == null || groupUser.Status != 1)
                {
                    return new OkObjectResult(new DataResponse("Tài khoản của bạn thuộc nhóm đã bị khóa hoặc không tồn tại"));
                }

                // gen token string
                var tokenModel = new JwtTokenDataModel()
                {
                    UserID = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email,
                    GroupID = user.GroupId
                };
                var token = jwtToken.GetJwtTokenString(tokenModel);

                return new OkObjectResult(new DataResponse(new
                {
                    FullName = user.FullName,
                    Token = token,
                }));
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult(new DataResponse(ex.ToString()));
            }
        }
    }
}
