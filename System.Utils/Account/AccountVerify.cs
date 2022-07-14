using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Utils.Account
{
    public static class AccountVerify
    {
        public static bool CheckAccount(string userName, ref string textError)
        {
            // Độ dài từ 4-16
            if (userName.Length < 4 || userName.Length > 16)
            {
                textError = "Tên tài khoản từ 4-16 ký tự và bắt đầu bằng chữ cái";
                return false;
            }

            //Kí tự đầu tiên phải là chữ cái, hoặc chữ số
            string fillterChar = "abcdefghijklmnopqrstuvxyzw";
            if (fillterChar.IndexOf(userName[0]) < 0)
            {
                textError = "Tên tài khoản từ 4-16 ký tự và bắt đầu bằng chữ cái";
                return false;
            }

            //Kí tự '.' không được xuất hiện liền nhau
            if (userName.IndexOf("..") >= 0)
            {
                textError = "Kí tự '..' không được xuất hiện liền nhau";
                return false;
            }

            //Kí tự '_' không được xuất hiện liền nhau
            if (userName.IndexOf("__") >= 0)
            {
                textError = "Kí tự '__' không được xuất hiện liền nhau";
                return false;
            }

            //Kí tự '._' không được xuất hiện liền nhau
            if (userName.IndexOf("._") >= 0)
            {
                textError = "Kí tự '._' không được xuất hiện liền nhau";
                return false;
            }

            //Kí tự '-.' không được xuất hiện liền nhau
            if (userName.IndexOf("_.") >= 0)
            {
                textError = "Kí tự '_.' không được xuất hiện liền nhau";
                return false;
            }

            // Ký tự '.' không được ở sau cùng
            if (userName.EndsWith("."))
            {
                textError = "Kí tự '.' không được ở sau cùng";
                return false;
            }

            // Ký tự '_' không được ở sau cùng
            if (userName.EndsWith("_"))
            {
                textError = "Kí tự '_' không được xuất hiện liền nhau";
                return false;
            }

            //Kí tự '-.' không được xuất hiện liền nhau
            if (userName.IndexOf(" ") >= 0)
            {
                textError = "Tài khoản không được chứa khoảng trắng";
                return false;
            }

            //Chuỗi hợp lệ   abcdefghijklmnopqrstuvxyzw012345678.
            fillterChar = "abcdefghijklmnopqrstuvxyzw0123456789._";
            for (int i = 0; i < userName.Length; i++)
            {
                if (fillterChar.IndexOf(userName[i]) < 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CheckPassword(string password, string rePassword, ref string textError)
        {
            if (password != rePassword)
            {
                textError = "Mật khẩu nhập lại không chính xác";
                return false;
            }
            if (password.Length < 4 || password.Length > 18)
            {
                textError = "Mật khẩu có độ dài từ 4-18 ký tự";
                return false;
            }

            if (password.IndexOf(" ") > -1)
            {
                textError = "Mật khẩu không được chứa khoảng trắng";
                return false;
            }

            return true;
        }
    }
}
