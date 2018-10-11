using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class CategoryOutput
    {
        public CategoryOutput(Category category)
        {
            this.Id = category.Id;
            this.Localized = category.Localized.Select(l => new LocalizedCategoryOutput(l)).ToArray();
            this.Children = category.Children.Select(c => new SubCategoryOutput(c)).ToArray();
        }

        public int Id { get; set; }

        public LocalizedCategoryOutput[] Localized { get; set; }

        public SubCategoryOutput[] Children { get; set; }
    }
}