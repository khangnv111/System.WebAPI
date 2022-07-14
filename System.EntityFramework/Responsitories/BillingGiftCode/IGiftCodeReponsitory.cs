using System.Models.Model.BillingGiftCode;

namespace System.EntityFramework.Responsitories.BillingGiftCode
{
    public interface IGiftCodeReponsitory
    {
        Task<GiftCode?> GetGiftCodeByIdAsync(int giftCodeId);
        Task<List<GiftCode>> GetGiftCodeListAsync(string giftCodeName, DateTime? fromDate, DateTime? toDate);
    }
}