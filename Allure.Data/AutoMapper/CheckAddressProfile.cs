using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.CheckAddresses;
using Allure.Core.Models;
using AutoMapper;

namespace Allure.Data.AutoMapper
{
    class CheckAddressProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<UpdateCheckAddress, CheckAddress>();
        }
    }
}
