using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class LocalizedHomePageImageOutput
    {
        public LocalizedHomePageImageOutput(LocalizedHomePageImage localized)
        {
            this.HomePageImageId = localized.HomePageImageId;
            this.LanguageCode = localized.LanguageCode;
            this.Title = localized.Title;
            this.Descrption = localized.Description;
        }

        public string HomePageImageId { get; set; }

        public string LanguageCode { get; set; }

        public string Title { get; set; }

        public string Descrption { get; set; }
    }
}