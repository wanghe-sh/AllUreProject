using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class OrderInput
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        public bool WillCheck { get; set; }

        public string CheckAddress { get; set; }

        public DateTime? CheckTime { get; set; }

        public string CheckContact { get; set; }

        public string LogisticCode { get; set; }

        public string LogisticOrderNumber { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Deposit { get; set; }

        public bool DepositReceipt { get; set; }

        public decimal? Remaining { get; set; }

        public bool RemainingReceipt { get; set; }

        public OrderDetailInput[] Details { get; set; }
    }
}