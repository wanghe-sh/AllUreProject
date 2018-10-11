using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public enum OrderStatus
    {
        New = 0,
        Verified = 1,
        Processing = 2,
        Cancelled = 3,
        Finished = 4
    }
}
