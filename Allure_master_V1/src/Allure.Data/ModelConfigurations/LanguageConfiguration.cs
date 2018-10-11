using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Data.ModelConfigurations
{
    class LanguageConfiguration : EntityTypeConfiguration<Language>
    {
        public LanguageConfiguration()
        {
            this.HasKey(l => l.Code);

            this.Property(l => l.Code).HasMaxLength(10);

            this.Property(l => l.IsDefault).IsRequired();

            this.Property(l => l.Enabled).IsRequired();
        }
    }
}
