using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Common.AutoMapper
{
    public abstract class MapAttributeBase : Attribute
    {
        public Type Converter { get; set; }
    }
}
