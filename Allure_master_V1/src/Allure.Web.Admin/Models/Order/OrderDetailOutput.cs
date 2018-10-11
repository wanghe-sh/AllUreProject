using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class OrderDetailOutput
    {
        public OrderDetailOutput(OrderDetail detail)
        {
            this.Id = detail.Id;
            this.OrderId = detail.OrderId;
            this.ProductId = detail.ProductId;            
            this.Count = detail.Count;
            this.Discount = detail.Discount;
            this.Product = new ProductOutput(detail.Product);
        }

        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public decimal Discount { get; set; }

        public decimal Count { get; set; }

        public ProductOutput Product { get; set; }
    }
}