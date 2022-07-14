using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.EntityFramework.Responsitories.Users;
using System.Linq;
using System.Models.ViewModel;
using System.Models.ViewModel.User;
using System.Text;
using System.Threading.Tasks;

namespace System.EntityFramework.Commands.Users
{
    public class UserGetListCommand
    {
        private readonly IUserReponsitory userReponsitory;
        private readonly IGroupReponsitory groupReponsitory;

        public UserGetListCommand(IUserReponsitory userReponsitory, IGroupReponsitory groupReponsitory)
        {
            this.userReponsitory = userReponsitory;
            this.groupReponsitory = groupReponsitory;
        }

        public async Task<IActionResult> ExecuteAsync(string userName, int status, CancellationToken cancellationToken)
        {
            if (status > 1 || status < -1)
            {
                return new OkObjectResult(new DataResponse("Input is invalid"));
            }
            userName = String.IsNullOrEmpty(userName) ? "" : userName;

            var listUser = await userReponsitory.GetUserListAsync(userName, status).ConfigureAwait(false);
            var listGroup = await groupReponsitory.GetGroupListAsync(-1).ConfigureAwait(false);

            var listData = from u in listUser
                           join gr in listGroup on u.GroupId equals gr.GroupId into userListData
                           from dt in userListData.DefaultIfEmpty()
                           select new
                           {
                               u.UserId,
                               u.UserName,
                               u.FullName,
                               u.Avatar,
                               u.Status,
                               u.Email,
                               u.GroupId,
                               u.Created,
                               u.Updated,
                               dt.GroupName
                           };

            return new OkObjectResult(new DataResponse(listData));
        }
    }
}
