using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.UI.Models
{
    public class LocalizedLocaleOutput
    {
        public LocalizedLocaleOutput(LocalizedLocale localizedLocale)
        {
            this.LanguageCode = localizedLocale.LanguageCode;
            this.Name = localizedLocale.Name;
        }

        public string LanguageCode { get; set; }

        public string Name { get; set; }
    }
}