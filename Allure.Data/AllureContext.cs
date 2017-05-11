using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Allure.Common.Unity;

namespace Allure.Data
{
    [DbConfigurationType(typeof(AllureConfiguration))]
    [Autowire(typeof(IDbContext), Lifetime.PerResolve)]
    public class AllureContext : DbContext, IDbContext
    {
        public AllureContext()
            : base("allure")
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Database.Log = s => System.Diagnostics.Trace.WriteLine(s);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(this.GetType().Assembly);
        }
    }
}
