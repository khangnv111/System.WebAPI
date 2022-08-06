using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.EntityFramework.Responsitories.BillingGiftCode;
using System.Linq;
using System.Models.Model.BillingGiftCode;
using System.Models.ViewModel;
using System.Models.ViewModel.GiftCode;
using System.Text;
using System.Threading.Tasks;
using System.Utils;

namespace System.EntityFramework.Commands.Giftcode
{
    public class GiftCodeAddCommand
    {
        private IGiftCodeReponsitory giftCode;
        private readonly IMapper mapper;
        private readonly ILogger<GiftCodeAddCommand> logger;

        public GiftCodeAddCommand(IGiftCodeReponsitory giftCode, IMapper mapper, ILogger<GiftCodeAddCommand> logger)
        {
            this.giftCode = giftCode;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IActionResult> ExecuteAsync(SaveGiftCode saveGiftCode, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(saveGiftCode.GiftCodeName) || string.IsNullOrEmpty(saveGiftCode.StartDate)
                    || string.IsNullOrEmpty(saveGiftCode.EndDate) || saveGiftCode.GiftCodeValue <= 0 || saveGiftCode.Quantity <= 0
                    || saveGiftCode.Quantity > 100000 || saveGiftCode.NumberInput < 0 || saveGiftCode.SourceID < 0 || saveGiftCode.SourceID > 4)
                {
                    return new OkObjectResult(new DataResponse("Input is invalid"));
                }

                var fromDate = DateTime.Parse(saveGiftCode.StartDate);
                var fromDateInt = Common.ConvertDateToInt(fromDate);

                var toDate = DateTime.Parse(saveGiftCode.EndDate);
                var toDateInt = Common.ConvertDateToInt(toDate);

                await Task.Yield();

                // create gitcode package
                var giftCodeModel = new GiftCode()
                {
                    GifCodeName = saveGiftCode.GiftCodeName,
                    GifCodeValue = saveGiftCode.GiftCodeValue,
                    Quantity = saveGiftCode.Quantity,
                    StartDate = fromDate,
                    EndDate = toDate,
                    StartDateInt = fromDateInt,
                    EndDateInt = toDateInt,
                    SourceID = saveGiftCode.SourceID,
                    NumberInput = saveGiftCode.NumberInput,
                    Status = 1
                };

                _ = await giftCode.AddGiftCodeAsync(giftCodeModel).ConfigureAwait(false);

                // get list giftcode data
                var listGiftCode = GenGiftcodeDataAsync(giftCodeModel);

                // save giftcode data
                _ = await giftCode.AddListGiftCodeDataAsync(listGiftCode).ConfigureAwait(false);

                var dataRecord = new DataRecord()
                {
                    Records = listGiftCode.Take(10).ToList(),
                    TotalRecords = listGiftCode.Count
                };

                return new OkObjectResult(new DataResponse(dataRecord));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return new NotFoundObjectResult(ex);
            }
        }

        private List<GiftCodeData> GenGiftcodeDataAsync(GiftCode data)
        {
            // list only giftcode
            var listGiftCode = new List<string>();

            // create list giftcode data
            var listData = new List<GiftCodeData>();
            var dem = 0;
            while (dem < data.Quantity)
            {
                var giftString = Common.RandomString(10);
                var giftCode = "P" + data.GifCodeID + "TS" + giftString;

                if (!CheckGiftCodeExistsAsync(giftCode, listGiftCode))
                {
                    listGiftCode.Add(giftCode);
                    var giftData = new GiftCodeData()
                    {
                        GifCodeID = data.GifCodeID,
                        GiftCode = giftCode,
                        Value = data.GifCodeValue,
                        IsUsed = false,
                        SourceID = data.SourceID,
                        Type = data.Type,
                        Status = 1,
                        NumberUsed = 0,
                        CreateTime = DateTime.Now,
                        StartDate = data.StartDate,
                        EndDate = data.EndDate,
                    };
                    listData.Add(giftData);

                    dem++;
                }
            }

            return listData;
        }

        private bool CheckGiftCodeExistsAsync(string giftCode, List<string> listGiftCode)
        {
            var check = listGiftCode.Any(x => x == giftCode);
            if (check)
            {
                return true;
            }
            return false;
        }
    }
}
