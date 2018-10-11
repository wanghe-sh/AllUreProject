using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class SearchProductStorageInput
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public int? BrandId { get; set; }

        public int? CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}