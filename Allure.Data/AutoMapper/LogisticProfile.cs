using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.Logistics;
using Allure.Core.Models;
using AutoMapper;

namespace Allure.Data.AutoMapper
{
    class LogisticProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AddLogistic, Logistic>();

            Mapper.CreateMap<UpdateLogistic, Logistic>();
        }
    }
}
