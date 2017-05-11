using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.Warehouses;
using Allure.Core.Models;
using AutoMapper;

namespace Allure.Data.AutoMapper
{
    class WarehouseProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AddWarehouse, Warehouse>();

            Mapper.CreateMap<UpdateWarehouse, Warehouse>();
        }
    }
}
