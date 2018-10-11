using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class LanguageOutput
    {
        public LanguageOutput(Language language)
        {
            this.Code = language.Code;
            this.Description = CultureInfo.GetCultureInfo(language.Code).DisplayName;
            this.IsDefault = language.IsDefault;
        }

        public string Code { get; set; }

        public string Description { get; set; }

        public bool IsDefault { get; set; }
    }
}