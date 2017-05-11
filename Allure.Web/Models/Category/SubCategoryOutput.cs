using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Web.Models
{
    public class SubCategoryOutput
    {
        public SubCategoryOutput(Category subCategory, string languageCode)
        {
            this.Id = subCategory.Id;
            //如果不设置子目录
            if (subCategory.ParentId != null)
            {
             this.ParentId = subCategory.ParentId.Value;
            }
            if (subCategory.Image != null)
            {
                this.ImageUrl = subCategory.Image.Url;
            }
            var localized = subCategory
                .Localizations
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

        public string SearchKey { get; set; }
    }
}