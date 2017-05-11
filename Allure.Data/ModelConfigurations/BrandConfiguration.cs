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
    class BrandConfiguration : EntityTypeConfiguration<Brand>
    {
        public BrandConfiguration()
        {
            this.HasKey(b => b.Id);

            this.Property(b => b.Name)
                .HasMaxLength(Config.MaxLength.Brand.Name)
                .IsIndex(unique: true);
        }
    }
}
