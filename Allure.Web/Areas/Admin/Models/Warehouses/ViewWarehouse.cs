﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;

namespace Allure.Web.Areas.Admin.Models.Warehouses
{
    [MapFrom(typeof(Warehouse))]
    public class ViewWarehouse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }
    }
}