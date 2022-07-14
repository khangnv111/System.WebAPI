using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.EntityFramework.Responsitories.Users;
using System.Linq;
using System.Models.ViewModel;
using System.Models.ViewModel.User;
using System.Text;
using System.Threading.Tasks;
using System.Utils.Auth;

namespace System.EntityFramework.Commands.Users
{
    public class UserGetInfoCommand
    {
        private readonly IUserReponsitory userReponsitory;
        private readonly IGroupReponsitory groupReponsitory;
        private readonly IPermissionReponsitory permissionReponsitory;
        private readonly JwtTokenServices jwtToken;

        public UserGetInfoCommand(IUserReponsitory userReponsitory, IGroupReponsitory groupReponsitory, 
            IPermissionReponsitory permissionReponsitory, JwtTokenServices jwtToken)
        {
            this.userReponsitory = userReponsitory;
            this.groupReponsitory = groupReponsitory;
            this.permissionReponsitory = permissionReponsitory;
            this.jwtToken = jwtToken;
        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            var userId = jwtToken.UserId;

            // get user
            var user = await userReponsitory.GetUserByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                return new OkObjectResult(new DataResponse("User not found"));
            }

            var groupId = jwtToken.GroupID;

            // get list permission by groupId
            var groupPermission = await permissionReponsitory.GetGroupPermissionByIdAsync(groupId).ConfigureAwait(false);
            var permissionList = await permissionReponsitory.GetPermissionListAsync(1).ConfigureAwait(false);

            var userPermissionData = from gr in groupPermission
                       join per in permissionList on gr.PermissionId equals per.PermissionId
                       //into GroupPermissionList
                       //from groupPer in GroupPermissionList.DefaultIfEmpty()
                       where per.Status == 1
                       select new
                       {
                           gr.GroupId,
                           gr.PermissionId,
                           gr.IsView,
                           gr.IsInsert,
                           gr.IsUpdate,
                           gr.IsDelete,

                           per.ParentId,
                           per.PermissionTitle,
                           per.PermissionKey,
                           per.PermissionUrl,
                           per.RedirectUrl,
                           per.Icon,
                           per.Position,
                       };

            return new OkObjectResult(new DataResponse(new
            {
                Avatar = user.Avatar,
                FullName = user.FullName,
                Email = user.Email,
                UserPermissionData = userPermissionData
            }));
        }
    }
}
