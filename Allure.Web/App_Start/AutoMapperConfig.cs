using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;
using AutoMapper;

namespace Allure.Web
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Bootstrapper.AutoMapping();
        }
    }
}