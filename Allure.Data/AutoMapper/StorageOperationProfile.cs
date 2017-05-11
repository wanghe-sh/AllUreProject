using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.StorageOperations;
using Allure.Core.Models;
using AutoMapper;

namespace Allure.Data.AutoMapper
{
    class StorageOperationProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AddStorageOperation, StorageOperation>();

            Mapper.CreateMap<AddStorageOperationDetail, StorageOperationDetail>();

            Mapper.CreateMap<UpdateStorageOperation, StorageOperation>();
        }
    }
}
