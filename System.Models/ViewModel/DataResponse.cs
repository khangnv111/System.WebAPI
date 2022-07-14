using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Models.ViewModel
{
    public class DataResponse
    {
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public dynamic? Data { get; set; } = null;

        public DataResponse() { }
        public DataResponse(string mess)
        {
            Code = -99;
            Message = mess;
        }

        public DataResponse(int code, string mess)
        {
            Code = code;
            Message = mess;
        }

        public DataResponse(dynamic data)
        {
            Message = "SUCCESS";
            Data = data;
        }

        public DataResponse(int code, string mess, dynamic data)
        {
            Code = code;
            Data = data;
            Message = mess;
        }
    }
}
