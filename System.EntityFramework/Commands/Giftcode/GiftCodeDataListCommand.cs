using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.EntityFramework.Responsitories.BillingGiftCode;
using System.Linq;
using System.Models.ViewModel;
using System.Text;
using System.Threading.Tasks;

namespace System.EntityFramework.Commands.Giftcode
{
    public class GiftCodeDataListCommand
    {
        private IGiftCodeReponsitory giftCode;
        public GiftCodeDataListCommand(IGiftCodeReponsitory giftCode)
        {
            this.giftCode = giftCode;
        }

        public async Task<IActionResult> ExecuteAsync(long giftCodeId, CancellationToken cancellationToken)
        {
            try
            {
                if (giftCodeId <= 0)
                {
                    return new OkObjectResult(new DataResponse("Input is invalid"));
                }

                // Get list giftcode data
                var giftCodeData = await giftCode.GetGiftCodeDataListAsync(giftCodeId).ConfigureAwait(false);

                var recordData = new DataRecord();
                recordData.TotalRecords = giftCodeData.Count;
                recordData.Records = giftCodeData;

                return new OkObjectResult(new DataResponse(recordData));
            }
            catch (Exception ex)
            {
                return new NotFoundObjectResult(ex);
            }


        }
    }
}
