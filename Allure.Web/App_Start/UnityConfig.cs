using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using Allure.Common.Unity;

namespace Allure.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.AddNewExtension<AutowireExtension>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}