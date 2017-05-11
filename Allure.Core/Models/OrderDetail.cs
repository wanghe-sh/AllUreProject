using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public decimal Count { get; set; }

        public decimal Delivered { get; set; }

        public decimal Discount { get; set; }

        public DateTime? DeliveryTime { get; set; }

        public virtual Product Product { get; set; }
    }
}
