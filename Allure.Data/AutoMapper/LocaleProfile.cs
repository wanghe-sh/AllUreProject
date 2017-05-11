using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria;
using Allure.Core.Criteria.Locales;
using Allure.Core.Models;
using AutoMapper;

namespace Allure.Data.AutoMapper
{
    class LocaleProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AddLocale, Locale>();

            Mapper.CreateMap<UpdateLocale, Locale>();

            Mapper.CreateMap<Localization, LocaleLocalization>();
        }
    }
}
