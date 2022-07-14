using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Models.ViewModel;
using System.Text;
using System.Threading.Tasks;

namespace System.EntityFramework.Commands.Giftcode
{
    public class GiftCodeListCommand
    {

        public GiftCodeListCommand()
        {
        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            

            return new OkObjectResult(new DataResponse());
        }
    }
}
