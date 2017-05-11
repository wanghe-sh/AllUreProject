using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Common.Unity
{
    public enum Lifetime
    {       
        Transient,
        PerThread,
        PerResolve,
        Singleton,
    }
}
