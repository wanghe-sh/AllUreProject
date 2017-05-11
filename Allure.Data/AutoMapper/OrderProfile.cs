using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.Orders;
using Allure.Core.Models;
using AutoMapper;

namespace Allure.Data.AutoMapper
{
    class OrderProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<UpdateOrder, Order>()
                .ForMember(o => o.Details, m => m.Ignore());

            Mapper.CreateMap<UpdateOrderDetail, OrderDetail>();
        }
    }
}
