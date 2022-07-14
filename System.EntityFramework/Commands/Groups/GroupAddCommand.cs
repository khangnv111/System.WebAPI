using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class GroupAddCommand
    {
        private readonly IGroupReponsitory groupReponsitory;
        private readonly IMapper mapper;

        public GroupAddCommand(IGroupReponsitory groupReponsitory, IMapper mapper)
        {
            this.groupReponsitory = groupReponsitory;
            this.mapper = mapper;
        }

        public async Task<IActionResult> ExecuteAsync(SaveGroup group, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(group.GroupName))
                {
                    return new OkObjectResult(new DataResponse("Input is invalid"));
                }

                // Create new group
                var groupModel = new Group()
                {
                    GroupName = group.GroupName,
                    Description = group.Description,
                    Status = (int)EnumGroupStatus.Active
                };

                _ = await groupReponsitory.AddGroupAsync(groupModel).ConfigureAwait(false);

                return new OkObjectResult(new DataResponse(groupModel));
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult(new DataResponse(ex.ToString()));
            }
        }
    }
}
