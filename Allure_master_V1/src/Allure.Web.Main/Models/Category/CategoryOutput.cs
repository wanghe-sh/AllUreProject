using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.UI.Models
{
    public class CategoryOutput
    {
        public CategoryOutput(Category category, string languageCode)
        {
            this.Id = category.Id;

            var localized = category
                .Localized
                .Where(l => l.LanguageCode.Equals(languageCode) || l.Language.IsDefault)
                .OrderBy(l => l.Language.IsDefault)
                .First();

            this.Localized = new LocalizedCategoryOutput(localized);

            this.Children = category
                .Children
                .Select(c => new SubCategoryOutput(c, languageCode))
                .ToArray();
        }

        public int Id { get; set; }

        public LocalizedCategoryOutput Localized { get; set; }

        public SubCategoryOutput[] Children { get; set; }
    }
}