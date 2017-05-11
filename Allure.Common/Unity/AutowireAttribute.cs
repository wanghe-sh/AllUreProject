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
        public AutowireAttribute(Type fromType, Lifetime lifeTime)
        {
            this.From = fromType;
            this.Lifetime = lifeTime;
        }

        public string Name { get; set; }

        public Type From { get; private set; }

        public Lifetime Lifetime { get; private set; }
    }
}
