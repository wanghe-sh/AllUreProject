using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class ProductOutput
    {
        public ProductOutput(Product product)
        {
            this.Id = product.Id;
            this.SubCategory = new SubCategoryOutput(product.SubCategory);
            this.Brand = product.Brand;
            this.Number = product.Number;
            this.Name = product.Name;
            this.Price = product.Price;
            this.DisplayOrder = product.DisplayOrder;
            this.Locale = new LocaleOutput(product.Locale);
            this.Localized = product.Localized.Select(l => new LocalizedProductOutput(l)).ToArray();
            this.Start = product.Start;
            this.End = product.End;
            this.Images = product.Images.Select(i => new ProductImageOutput(i)).ToArray();
            this.VideoUrl = product.VideoUrl;
            this.RecommendedS = product.RecommendedS;
            this.RecommendedL = product.RecommendedL;
        }

        public int Id { get; set; }

        public SubCategoryOutput SubCategory { get; set; }

        public Brand Brand { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int DisplayOrder { get; set; }

        public string VideoUrl { get; set; }

        public LocaleOutput Locale { get; set; }

        public LocalizedProductOutput[] Localized { get; set; }

        public ProductImageOutput[] Images { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public bool RecommendedS { get; set; }

        public bool RecommendedL { get; set; }
    }
}