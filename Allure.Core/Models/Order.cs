using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class Order
    {
        public Order()
        {
            this.Details = new List<OrderDetail>();
        }

        public int Id { get; set; }

        public string OrderNO { get; set; }

        public int CustomerId { get; set; }

        public bool? WillCheck { get; set; }
        
        public string CheckAddress { get; set; }

        public DateTime? CheckTime { get; set; }

        public string CheckContact { get; set; }

        public int DeliveryId { get; set; }

        public int? LogisticId { get; set; }

        public string LogisticOrderNumber { get; set; }

        public OrderStatus Status { get; set; }
        
        public decimal Discount { get; set; }

        public decimal? Deposit { get; set; }

        public bool DepositReceipt { get; set; }

        public DateTime? DepositDeadline { get; set; }

        public decimal? Remaining { get; set; }

        public bool RemainingReceipt { get; set; }

        public DateTime? RemainingDeadline { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public virtual User Customer { get; set; }

        public virtual UserDelivery Delivery { get; set; }

        public virtual Logistic Logistic { get; set; }

        public virtual ICollection<OrderDetail> Details { get; set; }
    }
}
