using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class LocaleOutput
    {
        public LocaleOutput(Locale locale)
        {
            this.Id = locale.Id;
            this.Status = locale.Status;
            this.Localized = locale.Localized.Select(l => new LocalizedLocaleOutput(l)).ToArray();
        }

        public int Id { get; set; }

        public DataStatus Status { get; set; }

        public LocalizedLocaleOutput[] Localized { get; set; }
    }
}