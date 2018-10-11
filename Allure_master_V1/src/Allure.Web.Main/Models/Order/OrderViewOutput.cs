using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.UI.Models
{
    public class OrderViewOutput
    {
        public OrderViewOutput(Order order, string languageCode)
        {
            this.Id = order.Id;
            this.WillCheck = order.WillCheck;
            this.CheckAddress = order.CheckAddress;
            this.CheckTime = order.CheckTime;
            this.CheckContact = order.CheckContact;
            this.ReceiverName = order.ReceiverName;
            this.ReceiverContact = order.ReceiverContact;
            this.ReceiverAddress = order.ReceiverAddress;
            this.ReceiverPostCode = order.ReceiverPostCode;
            this.LogisticCode = order.LogisticCode;
            this.LogisticOrderNumber = order.LogisticOrderNumber;
            this.Status = order.Status;
            this.OriginalRealCharge = order.OriginalRealCharge;
            this.Discount = order.Discount;
            this.RealCharge = order.RealCharge;
            this.Deposit = order.Deposit;
            this.DepositReceipt = order.DepositReceipt;
            this.Remaining = order.Remaining;
            this.RemainingRecept = order.RemainingReceipt;
            this.Details = order.Details.Select(d => new OrderDetailOutput(d, languageCode)).ToArray();            
            this.CreateTime = order.CreateTime;
            this.UpdateTime = order.UpdateTime;            
        }

        public int Id { get; set; }

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

        public OrderStatus Status { get; set; }

        public decimal OriginalRealCharge { get; set; }

        public decimal Discount { get; set; }

        public decimal RealCharge { get; set; }

        public decimal? Deposit { get; set; }

        public bool DepositReceipt { get; set; }

        public decimal? Remaining { get; set; }

        public bool RemainingRecept { get; set; }

        public OrderDetailOutput[] Details { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}