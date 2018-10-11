using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Services.UserService
{
    public interface ISysTextService
    {
        string GetText(string sysTextCode);
    }
}
