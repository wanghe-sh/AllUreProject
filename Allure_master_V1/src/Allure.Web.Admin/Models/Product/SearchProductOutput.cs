using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class SearchProductOutput
    {
        public int Count { get; set; }

        public ProductOutput[] Products { get; set; }
    }
}