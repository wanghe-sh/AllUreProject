using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Web.Areas.Admin.Models.Products
{
    public class ViewProductStorage
    {
        public int Id { get; set; }

        public decimal Current { get; set; }

        public decimal Frozen { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string BrandName { get; set; }

        public string CategoryName { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public TimeSpan? DeliveryTerm { get; set; }
    }
}