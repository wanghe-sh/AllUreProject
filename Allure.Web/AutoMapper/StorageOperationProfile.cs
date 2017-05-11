using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Allure.Core.Criteria;
using Allure.Core.Models;
using Allure.Web.Areas.Admin.Models.StorageOperations;
using AutoMapper;

namespace Allure.Web.AutoMapper
{
    public class StorageOperationProfile : Profile
    {
        protected override void Configure()
        {
            var createByResolver = CreateUserToFriendlyNameResolver(o => o.CreateBy);
            var updateByResolver = CreateUserToFriendlyNameResolver(o => o.UpdateBy);

            Mapper.CreateMap<StorageOperation, ViewStorageOperation>()
                .ForMember(v => v.CreateBy, m => m.ResolveUsing(createByResolver))
                .ForMember(v => v.UpdateBy, m => m.ResolveUsing(updateByResolver))
                .ForMember(v => v.OrderNO, m => m.MapFrom(o=>o.Order.OrderNO));

            Mapper.CreateMap<StorageOperationDetail, ViewStorageOperationDetail>()
                .ForMember(v => v.ProductNumber, m => m.MapFrom(o => o.Product.Number))
                .ForMember(v => v.ProductName, m => m.MapFrom(o => o.Product.Name))
                .ForMember(v => v.BrandName, m => m.MapFrom(o => o.Product.Brand.Name))
                .ForMember(v => v.CategoryName, m => m.MapFrom(o => o.Product.Category.Localizations.Single(l => l.Language.IsDefault).Name));

            Mapper.CreateMap<StorageOperation, ViewSearchStorageOperation>()
                .ForMember(v => v.LogisticName, m => m.MapFrom(o => o.Logistic.Name))
                .ForMember(v => v.WarehouseName, m => m.MapFrom(o => o.Warehouse.Name))
                .ForMember(v => v.CreateBy, m => m.ResolveUsing(createByResolver))
                .ForMember(v => v.UpdateBy, m => m.ResolveUsing(updateByResolver));

            Mapper.CreateMap<SearchResult<StorageOperation>, SearchResult<ViewSearchStorageOperation>>();
        }

        Func<StorageOperation, string> CreateUserToFriendlyNameResolver(Func<StorageOperation, User> userExp)
        {
            return o =>
            {
                var user = userExp(o);
                return $"{user.FirstName} {user.LastName}({user.Email})";
            };
        }
    }
}