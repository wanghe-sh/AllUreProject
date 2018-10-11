using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Allure.UI
{
    public class EncryptionService
    {
        public static string AesEncrypt(string text, byte[] key, byte[] iv)
        {
            using (var aes = new RijndaelManaged())
            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write))
            using (var writer = new StreamWriter(cs))
            {
                writer.Write(text);
                writer.Flush();
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static string AesDecrypt(string token, byte[] key, byte[] iv)
        {
            using (var aes = new RijndaelManaged())
            using (var ms = new MemoryStream(Convert.FromBase64String(token)))
            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read))
            using (var reader = new StreamReader(cs))
            {
                return reader.ReadToEnd();
            }
        }

        public static string ComputePasswordHash(string password)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            }
        }
    }
}