using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public enum OrderStatus
    {
        //Unsubmit = 0,
        //Submitted = 1,
        ToBeContact = 0,
        ToBeSeeGoods = 1,
        ToBePayTheDeposit = 2,
        ToBePayTheBalance = 3,
        Canceled = 4,
        ToBeShip = 5,
        Shipped = 6,
        Returned = 7
    }
}
