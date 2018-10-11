using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Services.EncryptionService
{
    public interface IEncryptionService
    {
        string AesEncrypt(string text, byte[] key, byte[] iv);

        string AesDecrypt(string token, byte[] key, byte[] iv);

        string ComputePasswordHash(string password);
    }
}
