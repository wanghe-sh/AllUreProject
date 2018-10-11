using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class LocalizedHomePageImageInput
    {
        public string LanguageCode { get; set; }

        public string Title { get; set; }

        public string Descrption { get; set; }
    }
}