using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Repositories
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
