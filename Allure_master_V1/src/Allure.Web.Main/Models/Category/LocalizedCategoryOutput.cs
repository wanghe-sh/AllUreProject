﻿using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.UI.Models
{
    public class LocalizedCategoryOutput
    {
        public LocalizedCategoryOutput(LocalizedCategory localizedCategory)
        {
            this.LanguageCode = localizedCategory.LanguageCode;
            this.Name = localizedCategory.Name;
            this.Description = localizedCategory.Description;
        }

        public string LanguageCode { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}