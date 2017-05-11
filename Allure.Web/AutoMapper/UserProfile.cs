using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Allure.Core.Criteria;
using Allure.Core.Models;
using Allure.Web.Areas.Admin.Models.Users;
using AutoMapper;

namespace Allure.Web.AutoMapper
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, ViewUser>()
                .ForMember(
                    v => v.Roles,
                    m => m.ResolveUsing<UserRoleToRole>());

            Mapper.CreateMap<User, ViewSearchUser>()
                .ForMember(
                    v => v.Roles,
                    m => m.ResolveUsing<UserRoleToRole>());

            Mapper.CreateMap<SearchResult<User>, SearchResult<ViewSearchUser>>();
        }

        class UserRoleToRole : ValueResolver<User, Role[]>
        {
            protected override Role[] ResolveCore(User source)
            {
                return source.Roles.Select(r => r.Role).ToArray();
            }
        }
    }
}