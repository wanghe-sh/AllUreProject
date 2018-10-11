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

        public decimal OriginalTotal
        {
            get
            {
                return this.Product.Price * this.Count;
            }
        }

        public decimal Discount { get; set; }

        public decimal RealCharge
        {
            get
            {
                return this.OriginalTotal - this.Discount;
            }
        }

        public virtual Product Product { get; set; }
    }
}
