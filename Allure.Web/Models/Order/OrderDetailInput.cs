using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Web.Models
{
    public class OrderDetailInput
    {
        public int ProductId { get; set; }

        public int Count { get; set; }
    }
}