using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.UI.Models
{
    public class LocaleOutput
    {
        public LocaleOutput(Locale locale, string languageCode)
        {
            this.Id = locale.Id;
            this.Localized = new LocalizedLocaleOutput(locale.Localized.Single(l => l.LanguageCode.Equals(languageCode)));
        }

        public int Id { get; set; }

        public LocalizedLocaleOutput Localized { get; set; }
    }
}