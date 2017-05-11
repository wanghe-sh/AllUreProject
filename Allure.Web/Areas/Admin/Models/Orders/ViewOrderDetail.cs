using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;

namespace Allure.Web.Areas.Admin.Models.Orders
{
    public class ViewOrderDetail
    {
        public int Id { get; set; }

        public decimal Count { get; set; }

        public decimal Delivered { get; set; }

        public decimal? Discount { get; set; }

        public DateTime? DeliveryTime { get; set; }

        public ViewProduct Product { get; set; }
    }

    public class ViewProduct
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string BrandName { get; set; }

        public decimal Current { get; set; }
    }
}