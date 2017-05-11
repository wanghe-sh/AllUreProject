using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.Images;
using Allure.Core.Models;
using AutoMapper;

namespace Allure.Data.AutoMapper
{
    class ImageProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AddImage, Image>();
        }
    }
}
