using Allure.Common;
using Allure.Common.Unity;
using Allure.Core.Repositories;
using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core
{
    [Autowire(typeof(ILoginService))]
    class LoginService : ILoginService
    {
        private readonly string _tokenDataSeparator = "|";
        private readonly IUnitOfWork _unitOfWork;

        public LoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        bool ILoginService.LoginWithPassword(string userName, string plainTextPassword, out User user)
        {
            var encryptedPassword = plainTextPassword.Md5();

            user = null;// _userRepository.Find(u => u.UserName == userName && u.EncryptedPassword == encryptedPassword);

            return user != null;
        }

        bool ILoginService.LoginWithToken(string token, out User user)
        {
            try
            {
                var parts = token.Split(_tokenDataSeparator);
                var userName = parts[0];
                var encryptedPassword = parts[1];
                var authDate = DateTime.Parse(parts[2]);

                user = null;// _userRepository.Find(u => u.UserName == userName && u.EncryptedPassword == encryptedPassword);

                return user != null;
            }
            catch
            {
                user = null;
                return false;
            }
        }

        string ILoginService.GenerateLoginToken(User user)
        {
            return string.Join(_tokenDataSeparator, user.Id, user.Password, DateTime.UtcNow.ToString());            
        }
    }
}
