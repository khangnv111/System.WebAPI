using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Models.Model.BillingGiftCode
{
    public class GiftCodeData
    {
        [Key]
        public long ID { get; set; }
        public long GifCodeID { get; set; }

        [Column("GifCode")]
        public string GiftCode { get; set; } = string.Empty;
        public int Value { get; set; }
        public int? EventID { get; set; }
        public bool IsUsed { get; set; }
        public int SourceID { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int NumberUsed { get; set; }
        public long? AccountID { get; set; }
        //public string AccountName { get; set; } = string.Empty;

        public DateTime? UsedTime { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
