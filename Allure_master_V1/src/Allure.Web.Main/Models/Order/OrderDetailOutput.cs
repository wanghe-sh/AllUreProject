using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.UI.Models
{
    public class OrderDetailOutput
    {
        public OrderDetailOutput(OrderDetail detail, string languageCode)
        {
            this.ProductId = detail.ProductId;
            this.ProductPrice = detail.Product.Price;            
            this.Count = detail.Count;
            this.OriginalTotal = detail.OriginalTotal;
            this.Discount = detail.Discount;
            this.RealCharge = detail.RealCharge;
            this.Product = new ProductOutput(detail.Product, languageCode);
        }

        public int ProductId { get; set; }

        public decimal ProductPrice { get; set; }

        public decimal Count { get; set; }

        public decimal OriginalTotal { get; set; }

        public decimal Discount { get; set; }

        public decimal RealCharge { get; set; }

        public ProductOutput Product { get; set; }
    }
}