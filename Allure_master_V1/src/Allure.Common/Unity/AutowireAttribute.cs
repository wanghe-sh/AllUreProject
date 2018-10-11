using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Common.Unity
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class AutowireAttribute : Attribute
    {
        public AutowireAttribute(Type fromType)
        {
            this.From = fromType;
            this.Lifetime = Lifetime.Transient;
        }

        public string Name { get; set; }

        public Type From { get; private set; }

        public Lifetime Lifetime { get; set; }
    }
}
