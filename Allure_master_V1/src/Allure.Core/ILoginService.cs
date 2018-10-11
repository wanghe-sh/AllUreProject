using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core
{
    public interface ILoginService
    {
        bool LoginWithPassword(string userName, string plainTextPassword, out User user);

        bool LoginWithToken(string token, out User user);

        string GenerateLoginToken(User user);
    }
}
