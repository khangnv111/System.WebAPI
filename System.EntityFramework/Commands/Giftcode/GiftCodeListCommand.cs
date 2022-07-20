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
    public class GiftCodeListCommand
    {
        private IGiftCodeReponsitory giftCode;
        public GiftCodeListCommand(IGiftCodeReponsitory giftCode)
        {
            this.giftCode = giftCode;
        }

        public async Task<IActionResult> ExecuteAsync(string giftCodeName, string fromDate, string toDate, CancellationToken cancellationToken)
        {
            try
            {
                var startDate = DateTime.Now.AddMonths(-3);
                if (!string.IsNullOrEmpty(fromDate))
                {
                    startDate = DateTime.Parse(fromDate);
                }
                var endDate = DateTime.Now;
                if (!string.IsNullOrEmpty(toDate))
                {
                    endDate = DateTime.Parse(toDate);
                }
                giftCodeName = string.IsNullOrEmpty(giftCodeName) ? "" : giftCodeName;

                // Get list giftcode
                var listGiftCode = await giftCode.GetGiftCodeListAsync(giftCodeName, startDate, endDate).ConfigureAwait(false);

                var recordData = new DataRecord();
                recordData.TotalRecords = listGiftCode.Count;
                recordData.Records = listGiftCode;

                return new OkObjectResult(new DataResponse(recordData));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }


        }
    }
}
