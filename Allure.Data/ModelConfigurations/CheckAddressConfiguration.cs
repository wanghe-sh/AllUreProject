using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core;
using System.Data.Entity.Infrastructure.Annotations;

namespace Allure.Data.ModelConfigurations
{
    class CheckAddressConfiguration : EntityTypeConfiguration<CheckAddress>
    {
        public CheckAddressConfiguration()
        {
            this.HasKey(b => b.Id);

            this.Property(b => b.Address)
                .HasMaxLength(Config.MaxLength.CheckAddress.Address)
                .IsIndex(unique: true);
        }
    }
}
