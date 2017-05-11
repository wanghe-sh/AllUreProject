using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Web.Models
{
    public class LocalizedLocaleOutput
    {
        public LocalizedLocaleOutput(LocaleLocalization localizedLocale)
        {
            this.LanguageCode = localizedLocale.LanguageCode;
            this.Name = localizedLocale.Name;
        }

        public string LanguageCode { get; set; }

        public string Name { get; set; }
    }
}