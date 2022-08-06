using System.Models.Model.BillingGiftCode;

namespace System.EntityFramework.Responsitories.BillingGiftCode
{
    public interface IGiftCodeReponsitory
    {
        Task<GiftCode> AddGiftCodeAsync(GiftCode giftcode);
        Task<GiftCodeData> AddGiftCodeDataAsync(GiftCodeData data);
        Task<List<GiftCodeData>> AddListGiftCodeDataAsync(List<GiftCodeData> data);
        Task<GiftCode?> GetGiftCodeByIdAsync(int giftCodeId);
        Task<List<GiftCodeData>> GetGiftCodeDataListAsync(long giftCodeId);
        Task<List<GiftCode>> GetGiftCodeListAsync(string giftCodeName, DateTime? fromDate, DateTime? toDate);
    }
}