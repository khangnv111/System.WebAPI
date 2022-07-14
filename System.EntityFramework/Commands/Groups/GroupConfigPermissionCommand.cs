using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.EntityFramework.Responsitories.Users;
using System.Linq;
using System.Models.Model.Admin;
using System.Models.ViewModel;
using System.Models.ViewModel.User;
using System.Text;
using System.Threading.Tasks;

namespace System.EntityFramework.Commands.Groups
{
    public class GroupConfigPermissionCommand
    {
        private readonly IGroupReponsitory groupReponsitory;
        private readonly IPermissionReponsitory permissionReponsitory;

        public GroupConfigPermissionCommand(IGroupReponsitory groupReponsitory, IPermissionReponsitory permissionReponsitory)
        {
            this.groupReponsitory = groupReponsitory;
            this.permissionReponsitory = permissionReponsitory;
        }

        public async Task<IActionResult> ExecuteAsync(SaveGroupPermission data, CancellationToken cancellationToken)
        {
            try
            {
                if (data.GroupId <= 0)
                {
                    return new OkObjectResult(new DataResponse("Input is invalid"));
                }

                // Check group exists
                var groupData = await groupReponsitory.GetGroupByIdAsync(data.GroupId).ConfigureAwait(false);
                if (groupData == null)
                {
                    return new OkObjectResult(new DataResponse("Nhóm không tồn tại"));
                }

                // Delete data exists
                await permissionReponsitory.DeleteGroupPermissionAsync(data.GroupId, cancellationToken).ConfigureAwait(false);

                // Insert
                var permissionList = new List<GroupPermission>();

                if (data.PermissionList != null && data.PermissionList.Count > 0)
                {
                    foreach (var item in data.PermissionList)
                    {
                        var permission = new GroupPermission()
                        {
                            GroupId = data.GroupId,
                            PermissionId = item.PermissionId,
                            IsView = item.IsView,
                            IsInsert = item.IsInsert,
                            IsUpdate = item.IsUpdate,
                            IsDelete = item.IsDelete,
                        };

                        permissionList.Add(permission);
                    }
                }

                await permissionReponsitory.AddGroupPermissionAsync(permissionList, cancellationToken).ConfigureAwait(false);

                return new OkObjectResult(new DataResponse(permissionList));
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult(new DataResponse(ex.ToString()));
            }
        }
    }
}
