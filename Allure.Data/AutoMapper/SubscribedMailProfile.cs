using Allure.Core.Criteria.SubscribedMails;
using Allure.Core.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Data.AutoMapper
{
    class SubscribedMailProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AddSubscribedMail, SubscribedMail>();
        }
    }
}
