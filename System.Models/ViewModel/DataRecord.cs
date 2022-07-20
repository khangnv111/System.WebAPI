using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Models.ViewModel
{
    public class DataRecord
    {
        public long TotalRecords { get; set; }
        public int TotalPage { get; set; }
        public dynamic? Records { get; set; }
    }
}
