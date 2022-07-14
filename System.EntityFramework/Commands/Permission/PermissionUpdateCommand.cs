using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.EntityFramework.Responsitories.Users;
using System.Linq;
using System.Models.ViewModel;
using System.Models.ViewModel.User;
using System.Text;
using System.Threading.Tasks;

namespace System.EntityFramework.Commands.Permission
{
    public class PermissionUpdateCommand
    {
        private readonly IPermissionReponsitory permissionReponsitory;

        public PermissionUpdateCommand(IPermissionReponsitory permissionReponsitory)
        {
            this.permissionReponsitory = permissionReponsitory;
        }

        public async Task<IActionResult> ExecuteAsync(int permissionId, SavePermission data, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(data.PermissionTitle) || string.IsNullOrEmpty(data.PermissionKey)
                    || data.ParentId < 0)
                {
                    return new OkObjectResult(new DataResponse("Input is invalid"));
                }

                var permissionInfo = await permissionReponsitory.GetPermissionByIdAsync(permissionId).ConfigureAwait(false);
                if (permissionInfo == null)
                {
                    return new OkObjectResult(new DataResponse("PermissionId not exists"));
                }
                
                // save update
                permissionInfo.ParentId = data.ParentId;
                permissionInfo.PermissionTitle = data.PermissionTitle;
                permissionInfo.PermissionKey = data.PermissionKey;
                permissionInfo.PermissionUrl = data.PermissionUrl;
                permissionInfo.RedirectUrl = data.RedirectUrl;
                permissionInfo.Icon = data.Icon;
                permissionInfo.Position = data.Position;
                permissionInfo.Status = data.Status;

                _ = await permissionReponsitory.UpdatePermissionAsync(permissionInfo, cancellationToken).ConfigureAwait(false);

                return new OkObjectResult(new DataResponse(data));
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult(new DataResponse(ex.ToString()));
            }
        }
    }
}
