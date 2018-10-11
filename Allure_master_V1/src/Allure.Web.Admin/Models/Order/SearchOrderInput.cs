using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class SearchOrderInput
    {
        public int? Id { get; set; }

        public OrderStatus? Status { get; set; }

        public decimal? MinTotalPrice { get; set; }

        public decimal? MaxTotalPrice { get; set; }

        public string CustomerName { get; set; }

        public DateTime? MinCreateTime { get; set; }

        public DateTime? MaxCreateTime { get; set; }

        public DateTime? MinUpdateTime { get; set; }

        public DateTime? MaxUpdateTime { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}