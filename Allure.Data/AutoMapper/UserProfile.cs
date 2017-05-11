using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.Users;
using Allure.Core.Models;
using AutoMapper;

namespace Allure.Data.AutoMapper
{
    class UserProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AddUser, User>()
                .ForMember(
                    u => u.Roles,
                    m => m.ResolveUsing(
                        a => a.Roles
                            .Select(r => new UserRole { Role = r })
                            .ToArray()));
            
            Mapper.CreateMap<UpdateUser, User>()
                .ForMember(u => u.Roles, m => m.Ignore())
                .ForMember(u => u.Deliveries, m => m.Ignore());

            Mapper.CreateMap<UpdateUserDelivery, UserDelivery>();
        }
    }
}
