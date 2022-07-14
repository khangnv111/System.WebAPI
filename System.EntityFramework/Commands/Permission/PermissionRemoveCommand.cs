using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.EntityFramework.Responsitories.Users;
using System.Linq;
using System.Models.ViewModel;
using System.Text;
using System.Threading.Tasks;

namespace System.EntityFramework.Commands.Permission
{
    public class PermissionRemoveCommand
    {
        private readonly IPermissionReponsitory permissionReponsitory;

        public PermissionRemoveCommand(IPermissionReponsitory permissionReponsitory)
        {
            this.permissionReponsitory = permissionReponsitory;
        }

        public async Task<IActionResult> ExecuteAsync(int permissionId, CancellationToken cancellationToken)
        {
            try
            {
                if (permissionId < 0)
                {
                    return new OkObjectResult(new DataResponse("Input is invalid"));
                }

                var permission = await permissionReponsitory.GetPermissionByIdAsync(permissionId).ConfigureAwait(false);
                if (permission == null)
                {
                    return new OkObjectResult(new DataResponse("Permission not exists"));
                }

                await permissionReponsitory.DeletePermissionAsync(permission, cancellationToken).ConfigureAwait(false);

                return new OkObjectResult(new DataResponse(permission));
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult(new DataResponse(ex.ToString()));
            }
        }
    }
}
