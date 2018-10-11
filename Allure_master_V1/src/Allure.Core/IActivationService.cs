using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core
{
    public interface IActivationService
    {
        string GenerateActivateToken(string userName);

        bool ValidateActivateToken(string userName, string token);
    }
}
