using Allure.Common;
using Allure.Core.Services;
using Allure.Core.Services.EncryptionService;
using Allure.Common.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core
{
    [Autowire(typeof(IActivationService))]
    public class ActivationService : IActivationService
    {        
        private byte[] _key = Encoding.UTF8.GetBytes("K*2c_bFa(*E$CVaD");
        private byte[] _iv = Encoding.UTF8.GetBytes("2McK*C3As(d^aC9@");

        private readonly IEncryptionService _encryptionService;

        public ActivationService(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        string IActivationService.GenerateActivateToken(string userName)
        {
            Contract.Requires(!userName.IsNullOrEmpty());

            return _encryptionService.AesEncrypt(userName, _key, _iv);
        }
                
        bool IActivationService.ValidateActivateToken(string userName, string token)
        {
            Contract.Requires(!userName.IsNullOrEmpty());
            Contract.Requires(!token.IsNullOrEmpty());

            var origin = _encryptionService.AesDecrypt(token, _key, _iv);

            return string.Equals(userName, origin);
        }
    }
}
