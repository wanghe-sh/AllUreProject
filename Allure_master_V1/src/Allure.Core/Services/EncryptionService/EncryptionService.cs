using Allure.Common.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Services.EncryptionService
{
    [Autowire(typeof(IEncryptionService))]
    public class EncryptionService : IEncryptionService
    {
        string IEncryptionService.AesEncrypt(string text, byte[] key, byte[] iv)
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

        string IEncryptionService.AesDecrypt(string token, byte[] key, byte[] iv)
        {
            using (var aes = new RijndaelManaged())
            using (var ms = new MemoryStream(Convert.FromBase64String(token)))
            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read))
            using (var reader = new StreamReader(cs))
            {
                return reader.ReadToEnd();
            }
        }

        string IEncryptionService.ComputePasswordHash(string password)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            }
        }
    }
}
