using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;

namespace Allure.Web.Areas.Admin.Models.Products
{
    [MapFrom(typeof(Product))]
    public class ViewSearchProduct
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}