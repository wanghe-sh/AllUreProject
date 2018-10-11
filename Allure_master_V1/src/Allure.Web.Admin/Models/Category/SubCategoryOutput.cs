using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class SubCategoryOutput
    {
        public SubCategoryOutput(SubCategory subCategory)
        {
            this.Id = subCategory.Id;
            this.ParentId = subCategory.ParentId;
            this.ImageUrl = subCategory.ImageUrl;
            this.Localized = subCategory.Localized.Select(l => new LocalizedSubCategoryOutput(l)).ToArray();
        }

        public int Id { get; set; }

        public int ParentId { get; set; }

        public string ImageUrl { get; set; }

        public LocalizedSubCategoryOutput[] Localized { get; set; }
    }
}