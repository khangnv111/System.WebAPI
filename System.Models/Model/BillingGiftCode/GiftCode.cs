using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Models.Model.BillingGiftCode
{
    public class GiftCode
    {
        [Key]
        public int GifCodeID { get; set; }

        [MaxLength(200)]
        public string GifCodeName { get; set; } = string.Empty;
        public int GifCodeValue { get; set; }
        public int Quantity { get; set; }
        public DateTime? StartDate { get; set; }
        public int StartDateInt { get; set; }
        public DateTime? EndDate { get; set; }
        public int EndDateInt { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public string CreateByStaff { get; set; } = string.Empty;
        public int Status { get; set; }

        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public string UpdateByStaff { get; set; } = string.Empty;
        public int Type { get;set; }
        public int QuantityReal { get; set; }
        public int SourceID { get; set; }
        public int NumberInput { get; set; }

    }

    public enum GiftCodeSource
    {
        ALL,
        WEB,
        APP,
        IOS,
        ANDROID
    }

    public enum GiftCodeType
    {
        FUND,
        BALANCE
    }
}
