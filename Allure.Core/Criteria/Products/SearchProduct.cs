using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Criteria.Products
{
    public class SearchProduct
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public int? BrandId { get; set; }

        public int? ParentCategoryId { get; set; }

        public int? CategoryId { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public string Locale { get; set; }

        public SortProductBy? OrderBy { get; set; }

        public bool Descending { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }

    public enum SortProductBy
    {
        Name,
        Price,        
        LocaleId,
        BrandId,
        CreateDate
    }
}
