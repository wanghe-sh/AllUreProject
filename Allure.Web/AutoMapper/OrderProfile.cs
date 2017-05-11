using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Allure.Core.Criteria;
using Allure.Core.Models;
using Allure.Web.Areas.Admin.Models.Orders;
using AutoMapper;

namespace Allure.Web.AutoMapper
{
    public class OrderProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Product, ViewProduct>()
                .ForMember(v => v.BrandName, m => m.MapFrom(p => p.Brand.Name))
                .ForMember(v => v.Current, m => m.ResolveUsing(p => (p.Storage?.Current).GetValueOrDefault()));

            Mapper.CreateMap<Order, ViewOrder>()
                .ForMember(v => v.Total, m => m.ResolveUsing<TotalValueResolver>());

            Mapper.CreateMap<OrderDetail, ViewOrderDetail>();

            Mapper.CreateMap<Order, ViewSearchOrder>()
                .ForMember(v => v.Total, m => m.ResolveUsing<TotalValueResolver>())
                .ForMember(v => v.CustomerName, m => m.ResolveUsing(o => $"{o.Customer.FirstName} {o.Customer.LastName}"));

            Mapper.CreateMap<SearchResult<Order>, SearchResult<ViewSearchOrder>>();
        }
    }

    class TotalValueResolver : ValueResolver<Order, decimal>
    {
        protected override decimal ResolveCore(Order order)
        {
            return order.Details.Sum(d => d.Product.Price * d.Count - d.Discount) - order.Discount;
        }
    }
}