using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class SubCategoryInput
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string ImageUrl { get; set; }

        public LocalizedSubCategoryInput[] Localized { get; set; } 
    }
}