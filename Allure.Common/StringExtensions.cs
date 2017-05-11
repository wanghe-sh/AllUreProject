using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Common
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static string[] Split(this string s, string seperator, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return s.Split(new[] { seperator }, options);
        }

        public static string Md5(this string s)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(s));

                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            }
        }
    }
}
