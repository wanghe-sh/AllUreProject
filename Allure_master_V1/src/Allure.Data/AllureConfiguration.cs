using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Data
{
    class AllureConfiguration : DbConfiguration
    {
        public AllureConfiguration()
        {
            this.SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
            this.SetDatabaseInitializer<AllureContext>(new AllureDbInitializer());
        }
    }
}
