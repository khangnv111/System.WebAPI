using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.EntityFramework.Responsitories.Users;
using System.Linq;
using System.Models.ViewModel;
using System.Text;
using System.Threading.Tasks;

namespace System.EntityFramework.Commands.Groups
{
    public class GroupGetListCommand
    {
        private readonly IGroupReponsitory groupReponsitory;
        private readonly ILogger<GroupGetListCommand> logger;
        public GroupGetListCommand(IGroupReponsitory groupReponsitory, ILogger<GroupGetListCommand> logger)
        {
            this.groupReponsitory = groupReponsitory;
            this.logger = logger;
        }

        public async Task<IActionResult> ExecuteAsync(int status, CancellationToken cancellationToken)
        {
            if (status > 1 || status < -1)
            {
                Console.WriteLine("GroupGetListCommand: Input is invalid");
                return new OkObjectResult(new DataResponse("Input is invalid"));
            }

            var listGroup = await groupReponsitory.GetGroupListAsync(status).ConfigureAwait(false);

            return new OkObjectResult(new DataResponse(listGroup));
        }
    }
}
