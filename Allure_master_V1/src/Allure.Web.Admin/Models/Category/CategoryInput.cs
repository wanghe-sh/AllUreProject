using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class CategoryInput
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public LocalizedCategoryInput[] Localized { get; set; }
    }
}