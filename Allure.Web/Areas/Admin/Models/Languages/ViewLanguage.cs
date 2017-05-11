using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;

namespace Allure.Web.Areas.Admin.Models.Languages
{
    [MapFrom(typeof(Language))]
    public class ViewLanguage
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public bool IsDefault { get; set; }

        public bool Enabled { get; set; }
    }
}