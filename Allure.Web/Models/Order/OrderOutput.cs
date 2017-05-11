using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Allure.Data;

namespace Allure.Web.Models
{
    public class OrderOutput
    {
        public OrderOutput(Order order)
        {
            this.Id = order.Id;
            this.OrderNO = order.OrderNO;
            this.Summary = string.Join(", ", order.Details.Select(d => d.Product.Name + " × " + d.Count.ToString("#0")));
            this.Status = order.Status;
            //this.RealCharge = order.RealCharge;
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
                        realCharge += product.Price*detail.Count;
                    }
                }
            }
            this.RealCharge = realCharge - order.Discount;
        }

        public int Id { get; set; }

        public string OrderNO { get; set; }

        public string Summary { get; set; }

        public OrderStatus Status { get; set; }

        public decimal RealCharge { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}