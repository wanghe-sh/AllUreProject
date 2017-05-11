using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;

namespace Allure.Web.Areas.Admin.Models.Orders
{
    public class ViewSearchOrder
    {
        public int Id { get; set; }

        public string OrderNO { get; set; }

        public OrderStatus Status { get; set; }

        public decimal Total { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public string CustomerName { get; set; }
    }
}