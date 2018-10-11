using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class LocalizedCategoryInput
    {
        public string LanguageCode { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}