using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;

namespace Allure.Web.Areas.Admin.Models.Brands
{
    [MapFrom(typeof(Brand))]
    public class ViewBrand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }
    }
}