using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Web.Models
{
    public class CategoryIndex
    {
        public SubCategoryOutput[] SubCategories { get; set; }

        public SubCategoryOutput Current { get; set; }

        public ProductOutput[] Products { get; set; }
    }
}