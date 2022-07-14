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
    public class PermissionAddCommand
    {
        private readonly IPermissionReponsitory permissionReponsitory;

        public PermissionAddCommand(IPermissionReponsitory permissionReponsitory)
        {
            this.permissionReponsitory = permissionReponsitory;
        }

        public async Task<IActionResult> ExecuteAsync(SavePermission permission, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(permission.PermissionTitle) || string.IsNullOrEmpty(permission.PermissionKey)
                    || permission.ParentId < 0)
                {
                    return new OkObjectResult(new DataResponse("Input is invalid"));
                }

                // Create new
                var dataModel = new Models.Model.Admin.Permission()
                {
                    ParentId = permission.ParentId,
                    PermissionTitle = permission.PermissionTitle,
                    PermissionKey = permission.PermissionKey,
                    PermissionUrl = permission.PermissionUrl,
                    RedirectUrl = permission.RedirectUrl,
                    Icon = permission.Icon,
                    Position = permission.Position,
                    Status = 1,
                };

                _ = await permissionReponsitory.AddPermissionAsync(dataModel, cancellationToken).ConfigureAwait(false);

                return new OkObjectResult(new DataResponse(dataModel));
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult(new DataResponse(ex.ToString()));
            }
        }
    }
}
