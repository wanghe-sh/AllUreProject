using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class ProductInput
    {
        public int Id { get; set; }

        public int SubCategoryId { get; set; }

        public int BrandId { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int LocaleId { get; set; }

        public int DisplayOrder { get; set; }

        public LocalizedProductInput[] Localized { get; set; }

        public ProductImageInput[] Images { get; set; }

        public string VideoUrl { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public bool RecommendedS { get; set; }

        public bool RecommendedL { get; set; }
    }
}