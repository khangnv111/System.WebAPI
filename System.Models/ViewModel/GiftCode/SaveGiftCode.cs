using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Models.ViewModel.GiftCode
{
    public class SaveGiftCode
    {
        public string GiftCodeName { get; set; } = string.Empty;
        public int GiftCodeValue { get; set; }
        public int Quantity { get; set; }
        public int NumberInput { get; set; }
        public int SourceID { get; set; }
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
    }
}
