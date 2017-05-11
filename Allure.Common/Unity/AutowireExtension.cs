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
            foreach (var type in Helper.GetRuntimeTypes())
            {
                foreach (AutowireAttribute autowire in type.GetCustomAttributes(typeof(AutowireAttribute)))
                {
                    var lifetime = CreateLifetimeManager(this.Container, autowire.Lifetime);
                    this.Container.RegisterType(autowire.From, type, autowire.Name, lifetime);
                }
            }
        }

        public static LifetimeManager CreateLifetimeManager(IUnityContainer container, Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.PerThread:                    
                    return container.Resolve<PerThreadLifetimeManager>();

                case Lifetime.PerResolve:
                    return container.Resolve<PerResolveLifetimeManager>();

                case Lifetime.Singleton:
                    return container.Resolve<ContainerControlledLifetimeManager>();

                case Lifetime.Transient:
                default:
                    return container.Resolve<TransientLifetimeManager>();
            }
        }
    }
}
