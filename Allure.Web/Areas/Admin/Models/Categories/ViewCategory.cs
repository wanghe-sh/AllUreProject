using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;
using Allure.Web.Areas.Admin.Models.Images;

namespace Allure.Web.Areas.Admin.Models.Categories
{
    [MapFrom(typeof(Category))]
    public class ViewCategory
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public ViewImage Image { get; set; }

        public ViewLocalization[] Localizations { get; set; }
                
        public ViewCategory[] Children { get; set; }

        public bool Deleted { get; set; }
    }
}