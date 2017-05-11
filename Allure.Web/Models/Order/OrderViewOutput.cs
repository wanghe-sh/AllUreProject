using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Allure.Data;

namespace Allure.Web.Models
{
    public class OrderViewOutput
    {
        public OrderViewOutput(Order order, string languageCode)
        {
            this.Id = order.Id;
            this.OrderNO = order.OrderNO;
            this.WillCheck = order.WillCheck ?? true;
            this.CheckAddress = order.CheckAddress;
            this.CheckTime = order.CheckTime;
            this.CheckContact = order.CheckContact;
            this.ReceiverName = order.Delivery.Receiver;
            this.ReceiverContact = order.Delivery.Phone;
            this.ReceiverAddress = order.Delivery.Address;
            this.ReceiverPostCode = order.Delivery.PostCode;
            this.LogisticCode = order.Logistic == null ? string.Empty: order.Logistic.Name;
            this.LogisticOrderNumber = order.LogisticOrderNumber;
            this.Status = order.Status;
            this.Discount = order.Discount;
            this.Deposit = order.Deposit;
            this.DepositReceipt = order.DepositReceipt;
            this.DepositDeadline = order.DepositDeadline;
            this.Remaining = order.Remaining;
            this.RemainingRecept = order.RemainingReceipt;
            this.RemainingDeadline = order.RemainingDeadline;
            this.Details = order.Details.Select(d => new OrderDetailOutput(d, languageCode)).ToArray();
            this.CreateTime = order.CreateTime;
            this.UpdateTime = order.UpdateTime;

            decimal realCharge = 0;

            using (var dbContext = new AllureContext())
            {
                foreach (var detail in order.Details)
                {
                    var product = dbContext.Set<Product>().FirstOrDefault(p => p.Id == detail.ProductId);
                    if (product != null)
                    {
                        realCharge += product.Price * detail.Count;
                    }
                }
            }
            this.RealCharge = realCharge - order.Discount;
            this.OriginalRealCharge = realCharge;

        }

        public int Id { get; set; }

        public string OrderNO { get; set; }

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

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DepositDeadline { get; set; }

        public decimal? Remaining { get; set; }

        public bool RemainingRecept { get; set; }

        public DateTime? RemainingDeadline { get; set; }

        public OrderDetailOutput[] Details { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}