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
        ToBeContact = 0, // 待联系
        ToBeSeeGoods = 1, //待看货
        ToBePayTheDeposit = 2, //待付定金
        ToBePayTheBalance = 3, //待付余额
        Canceled = 4, //取消
        ToBeShip = 5, //待送货
        Shipped = 6, //已送货
        Returned = 7 //退货
    }
}
