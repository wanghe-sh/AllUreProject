using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Web.Models
{
    public class LocalizedSubCategoryOutput
    {
        public LocalizedSubCategoryOutput(CategoryLocalization localizedSubCategory)
        {
            this.LanguageCode = localizedSubCategory.LanguageCode;
            this.Name = localizedSubCategory.Name;
            this.Description = localizedSubCategory.Description;
        }

        public string LanguageCode { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}