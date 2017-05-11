using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;
using Allure.Web.Areas.Admin.Models.Users;

namespace Allure.Web.Areas.Admin.Models.Orders
{
    public class ViewOrder
    {
        public int Id { get; set; }

        public string OrderNO { get; set; }

        public bool? WillCheck { get; set; }

        public string CheckAddress { get; set; }

        public DateTime? CheckTime { get; set; }

        public string CheckContact { get; set; }

        public int? LogisticId { get; set; }

        public string LogisticOrderNumber { get; set; }

        public OrderStatus Status { get; set; }

        public decimal Discount { get; set; }

        public decimal Total { get; set; }

        public decimal? Deposit { get; set; }

        public bool DepositReceipt { get; set; }

        public DateTime? DepositDeadline { get; set; }

        public decimal? Remaining { get; set; }

        public bool RemainingReceipt { get; set; }

        public DateTime? RemainingDeadline { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public ViewUserDelivery Delivery { get; set; }

        public ViewOrderDetail[] Details { get; set; }
    }
}