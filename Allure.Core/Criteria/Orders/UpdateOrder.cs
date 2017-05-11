using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Models;
using Allure.Common.Validations;

namespace Allure.Core.Criteria.Orders
{
    public class UpdateOrder
    {
        [MaxLength(Config.MaxLength.Order.OrderNO)]
        [Required]
        public string OrderNO { get; set; }

        [Required]
        public OrderStatus? Status { get; set; }

        [Required]
        public bool? WillCheck { get; set; }

        [MaxLength(Config.MaxLength.Order.CheckAddress)]
        public string CheckAddress { get; set; }

        public DateTime? CheckTime { get; set; }

        [MaxLength(Config.MaxLength.Order.CheckContact)]
        public string CheckContact { get; set; }
        
        public int? LogisticId { get; set; }

        [MaxLength(Config.MaxLength.Order.LogisticOrderNumber)]
        public string LogisticOrderNumber { get; set; }

        [Required]
        [MinLength(1)]
        [ValidateChildren]
        public UpdateOrderDetail[] Details { get; set; }

        public decimal? Discount { get; set; }

        public decimal? Deposit { get; set; }

        public bool DepositReceipt { get; set; }

        public DateTime? DepositDeadline { get; set; }

        public decimal? Remaining { get; set; }

        public bool RemainingReceipt { get; set; }

        public DateTime? RemainingDeadline { get; set; }
    }

    public class UpdateOrderDetail
    {
        public int? Id { get; set; }

        [Required]
        public int? ProductId { get; set; }

        [Required]
        public decimal? Count { get; set; }

        public DateTime? DeliveryTime { get; set; }
    }
}
