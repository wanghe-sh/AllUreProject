﻿using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Data.ModelConfigurations
{
    class WarehouseConfiguration : EntityTypeConfiguration<Warehouse>
    {
        public WarehouseConfiguration()
        {
            this.HasKey(l => l.Code);
        }
    }
}
