using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.UI.Models
{
    public class OrderOutput
    {
        public OrderOutput(Order order)
        {
            this.Id = order.Id;
            this.Summary = string.Join(",", order.Details.Select(d => d.Product.Name + " × " + d.Count));
            this.Status = order.Status;
            this.RealCharge = order.RealCharge;
            this.CreateTime = order.CreateTime;
        }

        public int Id { get; set; }

        public string Summary { get; set; }

        public OrderStatus Status { get; set; }

        public decimal RealCharge { get; set; }

        public DateTime CreateTime { get; set; }
    }
}