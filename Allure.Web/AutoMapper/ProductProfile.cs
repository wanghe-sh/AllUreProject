using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Criteria;
using Allure.Core.Models;
using Allure.Web.Areas.Admin.Models.Products;
using AutoMapper;

namespace Allure.Web.AutoMapper
{
    public class ProductProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Product, ViewProduct>();

            Mapper.CreateMap<Product, ViewSearchProduct>()
                .ForMember(
                    v => v.ImageUrl,
                    m => m.ResolveUsing(p => p.Images.FirstOrDefault()?.Url));

            Mapper.CreateMap<SearchResult<Product>, SearchResult<ViewSearchProduct>>();

            Mapper.CreateMap<Product, ViewProductStorage>()
                .ForMember(s => s.Current, m => m.ResolveUsing(p => (p.Storage?.Current).GetValueOrDefault()))
                .ForMember(s => s.Frozen, m => m.ResolveUsing(p => (p.Storage?.Frozen).GetValueOrDefault()))
                .ForMember(s => s.BrandName, m => m.MapFrom(p => p.Brand.Name))
                .ForMember(s => s.CategoryName, m => m.ResolveUsing(p => p.Category.Localizations.Single(l => l.Language.IsDefault).Name));

            Mapper.CreateMap<SearchResult<Product>, SearchResult<ViewProductStorage>>();
        }
    }
}