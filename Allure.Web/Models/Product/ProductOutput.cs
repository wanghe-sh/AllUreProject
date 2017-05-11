using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Web.Models
{
    public class ProductOutput
    {
        public ProductOutput(Product product, string languageCode)
        {
            this.Id = product.Id;
            this.SubCategory = new SubCategoryOutput(product.Category, languageCode);
            this.SubCategory.Parent = new CategoryOutput(product.Category, languageCode);
            this.Brand = product.Brand;
            this.Number = product.Number;
            this.Name = product.Name;
            this.Price = product.Price;
            this.Locale = new LocaleOutput(product.Locale, languageCode);
            var localized = product
                .Localizations
                .Where(l => l.LanguageCode.Equals(languageCode) || l.Language.IsDefault)
                .OrderBy(l => l.Language.IsDefault)
                .First();
            this.Localized = new LocalizedProductOutput(localized);
            this.Start = product.Start;
            this.End = product.End;
            this.Images = product.Images.ToArray();
            this.RecommendedL = product.RecommendedL;
            this.RecommendedS = product.RecommendedS;
            this.VideoUrl = product.VideoUrl;
            this.OriginalPrice = product.OriginalPrice;
            this.Width = product.Width;
            this.Height = product.Height;
            this.Length = product.Length;
            this.EnglishName = product.EnglishName;

        }

        public int Id { get; set; }

        public SubCategoryOutput SubCategory { get; set; }

        public Brand Brand { get; set; }
                
        public string Number { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public LocaleOutput Locale { get; set; }

        public LocalizedProductOutput Localized { get; set; }

        public Image[] Images { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public bool RecommendedL { get; set; }

        public bool RecommendedS { get; set; }

        public string VideoUrl { get; set; }

        //new 
        public decimal OriginalPrice { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Length { get; set; }
        public string EnglishName { get; set; }
    }
}