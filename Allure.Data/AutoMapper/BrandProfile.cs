using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.Brands;
using Allure.Core.Models;
using AutoMapper;

namespace Allure.Data.AutoMapper
{
    class BrandProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AddBrand, Brand>();

            Mapper.CreateMap<UpdateBrand, Brand>();
        }
    }
}
