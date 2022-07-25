using System;
using System.Collections.Generic;
using System.EntityFramework.Data;
using System.Linq;
using System.Models.Model.BillingGiftCode;
using System.Text;
using System.Threading.Tasks;
using System.Utils;

namespace System.EntityFramework.Responsitories.BillingGiftCode
{
    public class GiftCodeReponsitory : IGiftCodeReponsitory
    {
        private readonly GiftCodeDbContext dbContext;
        public GiftCodeReponsitory(GiftCodeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region GiftCode
        public Task<List<GiftCode>> GetGiftCodeListAsync(string giftCodeName, DateTime? fromDate, DateTime? toDate)
        {
            var list = dbContext.GifCode.OrderByDescending(x => x.CreateTime)
                .Where(x => x.GifCodeName.Contains(giftCodeName))
                .If(fromDate.HasValue, x => x.Where(y => y.CreateTime >= fromDate!.Value))
                .If(toDate.HasValue, x => x.Where(y => y.CreateTime <= toDate!.Value)).ToList();

            return Task.FromResult(list);
        }

        public Task<GiftCode?> GetGiftCodeByIdAsync(int giftCodeId)
        {
            var giftCode = dbContext.GifCode.FirstOrDefault(x => x.GifCodeID == giftCodeId);
            return Task.FromResult(giftCode);
        }
        #endregion

        #region GiftCode Data
        public Task<List<GiftCodeData>> GetGiftCodeDataListAsync(long giftCodeId)
        {
            var list = dbContext.GifCodeData.Take(1000).ToList();
            //var list = dbContext.GifCodeData.Where(x => x.GifCodeID == giftCodeId).ToList();

            return Task.FromResult(list);
        }
        #endregion
    }
}
