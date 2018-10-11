using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.UI.Models
{
    public class SubCategoryOutput
    {
        public SubCategoryOutput(SubCategory subCategory, string languageCode)
        {
            this.Id = subCategory.Id;
            this.ParentId = subCategory.ParentId;
            this.ImageUrl = subCategory.ImageUrl;
            var localized = subCategory
                .Localized
                .Where(l => l.LanguageCode.Equals(languageCode) || l.Language.IsDefault)
                .OrderBy(l => l.Language.IsDefault)
                .First();
            this.Localized = new LocalizedSubCategoryOutput(localized);
        }

        public int Id { get; set; }

        public int ParentId { get; set; }

        public string ImageUrl { get; set; }

        public LocalizedSubCategoryOutput Localized { get; set; }

        public CategoryOutput Parent { get; set; }
    }
}