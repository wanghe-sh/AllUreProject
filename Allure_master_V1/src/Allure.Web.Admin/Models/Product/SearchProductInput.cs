using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class SearchProductInput
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public int? BrandId { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string Locale { get; set; }

        public string OrderBy { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}