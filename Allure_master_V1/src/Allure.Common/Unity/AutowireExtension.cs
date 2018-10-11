using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Reflection;
using System.IO;

namespace Allure.Common.Unity
{
    public class AutowireExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            foreach (var assembly in AutowireHelper.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    foreach (AutowireAttribute autowire in type.GetCustomAttributes(typeof(AutowireAttribute)))
                    {
                        var lifetime = AutowireHelper.CreateLifetimeManager(this.Container, autowire.Lifetime);
                        this.Container.RegisterType(autowire.From, type, autowire.Name, lifetime);                        
                    }
                }
            }
        }
    }
}
