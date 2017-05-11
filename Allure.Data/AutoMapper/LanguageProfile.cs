using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.Languages;
using Allure.Core.Models;
using AutoMapper;

namespace Allure.Data.AutoMapper
{
    class LanguageProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AddLanguage, Language>();

            Mapper.CreateMap<UpdateLanguage, Language>();
        }
    }
}
