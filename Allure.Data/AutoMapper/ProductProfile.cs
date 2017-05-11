using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria;
using Allure.Core.Criteria.Products;
using Allure.Core.Models;
using AutoMapper;

namespace Allure.Data.AutoMapper
{
    class ProductProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AddProduct, Product>();

            Mapper.CreateMap<UpdateProduct, Product>();

            Mapper.CreateMap<Localization, ProductLocalization>();
        }
    }
}
