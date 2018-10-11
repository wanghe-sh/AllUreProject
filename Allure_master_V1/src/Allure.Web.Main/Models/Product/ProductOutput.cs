using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.UI.Models
{
    public class ProductOutput
    {
        public ProductOutput(Product product, string languageCode)
        {
            this.Id = product.Id;
            this.SubCategory = new SubCategoryOutput(product.SubCategory, languageCode);
            this.SubCategory.Parent = new CategoryOutput(product.SubCategory.Parent, languageCode);
            this.Brand = product.Brand;
            this.Number = product.Number;
            this.Name = product.Name;
            this.Price = product.Price;
            this.Locale = new LocaleOutput(product.Locale, languageCode);
            var localized = product
                .Localized
                .Where(l => l.LanguageCode.Equals(languageCode) || l.Language.IsDefault)
                .OrderBy(l => l.Language.IsDefault)
                .First();
            this.Localized = new LocalizedProductOutput(localized);
            this.Start = product.Start;
            this.End = product.End;
            this.Images = product.Images.ToArray();
            this.RecommendedL = product.RecommendedL;
            this.RecommendedS = product.RecommendedS;
        }

        public int Id { get; set; }

        public SubCategoryOutput SubCategory { get; set; }

        public Brand Brand { get; set; }
                
        public string Number { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public LocaleOutput Locale { get; set; }

        public LocalizedProductOutput Localized { get; set; }

        public ProductImage[] Images { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public bool RecommendedL { get; set; }

        public bool RecommendedS { get; set; }
    }
}