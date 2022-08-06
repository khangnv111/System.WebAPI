using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System.Utils
{
    public static class Common
    {
        public static string MD5String(string Str)
        {
            if (string.IsNullOrEmpty(Str))
                return "";

            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(Str);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }

        public static int ConvertDateToInt(DateTime? date)
        {
            if (date == null)
                return 0;

            var Year = date.Value.Year;
            var Month = date.Value.Month;
            var Day = date.Value.Day;

            return (Year * 100 + Month) * 100 + Day;
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
