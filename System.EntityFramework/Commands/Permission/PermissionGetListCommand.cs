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
    public class PermissionGetListCommand
    {
        private readonly IPermissionReponsitory permissionReponsitory;
        public PermissionGetListCommand(IPermissionReponsitory permissionReponsitory)
        {
            this.permissionReponsitory = permissionReponsitory;
        }

        public async Task<IActionResult> ExecuteAsync(int? status, CancellationToken cancellationToken)
        {
            status = status != null ? status : -1;
            if (status > 1 || status < -1)
            {
                return new OkObjectResult(new DataResponse("Input is invalid"));
            }

            var list = await permissionReponsitory.GetPermissionListAsync((int)status).ConfigureAwait(false);

            return new OkObjectResult(new DataResponse(list));
        }
    }
}
