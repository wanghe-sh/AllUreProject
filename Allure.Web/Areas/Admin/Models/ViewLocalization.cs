using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;

namespace Allure.Web.Areas.Admin.Models
{
    [MapFrom(typeof(CategoryLocalization))]
    [MapFrom(typeof(LocaleLocalization))]
    [MapFrom(typeof(ProductLocalization))]
    public class ViewLocalization
    {
        public string LanguageCode { get; set; }
     
        public string Name { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public string DeliveryTerm { get; set; }
    }
}