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
    public class GroupUpdateCommand
    {
        private readonly IGroupReponsitory groupReponsitory;

        public GroupUpdateCommand(IGroupReponsitory groupReponsitory)
        {
            this.groupReponsitory = groupReponsitory;
        }

        public async Task<IActionResult> ExecuteAsync(int groupId, SaveGroup group, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(group.GroupName) || groupId <= 0)
                {
                    return new OkObjectResult(new DataResponse("Input is invalid"));
                }

                // Check group exists
                var groupData = await groupReponsitory.GetGroupByIdAsync(groupId);
                if (groupData == null)
                {
                    return new OkObjectResult(new DataResponse("Nhóm không tồn tại"));
                }

                // Update Group
                groupData.GroupName = group.GroupName;
                groupData.Description = group.Description;

                _ = await groupReponsitory.UpdateGroupAsync(groupData).ConfigureAwait(false);

                return new OkObjectResult(new DataResponse(group));
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult(new DataResponse(ex.ToString()));
            }
        }
    }
}
