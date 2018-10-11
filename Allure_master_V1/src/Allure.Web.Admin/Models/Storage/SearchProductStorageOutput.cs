using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class SearchProductStorageOutput
    {
        public int Count { get; set; }

        public ProductStorageOutput[] Storages { get; set; }
    }
}