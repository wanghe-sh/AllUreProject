using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class OrderOutput
    {
        public OrderOutput(Order order)
        {
            this.Id = order.Id;
            this.CustomerId = order.CustomerId;
            this.Status = order.Status;
            this.WillCheck = order.WillCheck;
            this.CheckAddress = order.CheckAddress;
            this.CheckTime = order.CheckTime;
            this.CheckContact = order.CheckContact;
            this.ReceiverContact = order.ReceiverContact;
            this.ReceiverAddress = order.ReceiverAddress;
            this.ReceiverName = order.ReceiverName;
            this.ReceiverPostCode = order.ReceiverPostCode;
            this.LogisticCode = order.LogisticCode;
            this.LogisticOrderNumber = order.LogisticOrderNumber;
            this.Discount = order.Discount;
            this.Deposit = order.Deposit;
            this.DepositReceipt = order.DepositReceipt;
            this.Remaining = order.Remaining;
            this.RemainingReceipt = order.RemainingReceipt;
            this.CreateTime = order.CreateTime;
            this.UpdateTime = order.UpdateTime;
            this.Customer = new UserOutput(order.Customer);
            this.Details = order.Details.Select(d => new OrderDetailOutput(d)).ToArray();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public OrderStatus Status { get; set; }

        public bool WillCheck { get; set; }

        public string CheckAddress { get; set; }

        public DateTime? CheckTime { get; set; }

        public string CheckContact { get; set; }

        public string ReceiverName { get; set; }

        public string ReceiverContact { get; set; }

        public string ReceiverAddress { get; set; }

        public string ReceiverPostCode { get; set; }

        public string LogisticCode { get; set; }

        public string LogisticOrderNumber { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Deposit { get; set; }

        public bool DepositReceipt { get; set; }

        public decimal? Remaining { get; set; }

        public bool RemainingReceipt { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public UserOutput Customer { get; set; }

        public OrderDetailOutput[] Details { get; set; }
    }
}