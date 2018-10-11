using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

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

        public bool RemainingReceipt { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public virtual User Customer { get; set; }

        public virtual ICollection<OrderDetail> Details { get; set; }
    }
}
