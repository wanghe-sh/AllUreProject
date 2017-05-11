using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;

namespace Allure.Web.Areas.Admin.Models.Locales
{
    [MapFrom(typeof(Locale))]
    public class ViewLocale
    {
        public int Id { get; set; }

        public ViewLocalization[] Localizations { get; set; }

        public bool Deleted { get; set; }
    }
}