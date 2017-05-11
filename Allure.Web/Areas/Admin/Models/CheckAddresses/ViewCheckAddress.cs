using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;

namespace Allure.Web.Areas.Admin.Models.CheckAddresses
{
    [MapFrom(typeof(CheckAddress))]
    public class ViewCheckAddress
    {
        public int Id { get; set; }

        public string Address { get; set; }
        
    }
}