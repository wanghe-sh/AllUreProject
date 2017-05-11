using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Mappers;

namespace Allure.Common.AutoMapper
{
    public static class Bootstrapper
    {
        public static void AutoMapping()
        {
            foreach (var type in Helper.GetRuntimeTypes())
            {
                if (type.IsSubclassOf(typeof(Profile)) && type.FullName.StartsWith("Allure"))
                {
                    var profile = (Profile)Activator.CreateInstance(type);
                    Mapper.AddProfile(profile);
                }

                foreach (MapAttributeBase attr in type.GetCustomAttributes(typeof(MapAttributeBase), false))
                {
                    var from = attr as MapFromAttribute;
                    var to = attr as MapToAttribute;
                    IMappingExpression mapping = null;

                    if (from != null)
                    {
                        mapping = Mapper.CreateMap(from.Type, type);
                    }

                    if (to != null)
                    {
                        mapping = Mapper.CreateMap(type, to.Type);
                    }

                    if (mapping != null && attr.Converter != null)
                    {
                        mapping.ConvertUsing(attr.Converter);
                    }
                }
            }
        }
    }
}
