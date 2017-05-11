using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria;
using Allure.Core.Criteria.Categories;
using Allure.Core.Models;
using AutoMapper;

namespace Allure.Data.AutoMapper
{
    class CategoryProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AddCategory, Category>();

            Mapper.CreateMap<UpdateCategory, Category>();

            Mapper.CreateMap<Localization, CategoryLocalization>();
        }
    }
}
