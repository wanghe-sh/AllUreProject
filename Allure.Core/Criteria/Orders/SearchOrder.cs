using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Models;

namespace Allure.Core.Criteria.Orders
{
    public class SearchOrder
    {
        public int? orderId { get; set; }

        public string OrderNo { get; set; }

        public OrderStatus? Status { get; set; }

        public string Customer { get; set; }

        public decimal? MinTotal { get; set; }

        public decimal? MaxTotal { get; set; }

        public DateTime? MinCreateTime { get; set; }

        public DateTime? MaxCreateTime { get; set; }

        public DateTime? MinUpdateTime { get; set; }

        public DateTime? MaxUpdateTime { get; set; }

        public SortOrderBy? SortBy { get; set; }

        public bool Descending { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }

    public enum SortOrderBy
    {
        CreateTime,
        UpdateTime,
        Status
    }
}
