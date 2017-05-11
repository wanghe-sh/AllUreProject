using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Common.AutoMapper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, AllowMultiple = true, Inherited = false)]
    public class MapToAttribute : MapAttributeBase
    {
        public Type Type { get; private set; }

        public MapToAttribute(Type to)
        {
            this.Type = to;
        }
    }
}
