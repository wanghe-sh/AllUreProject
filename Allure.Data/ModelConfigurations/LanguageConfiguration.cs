using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core;

namespace Allure.Data.ModelConfigurations
{
    class LanguageConfiguration : EntityTypeConfiguration<Language>
    {
        public LanguageConfiguration()
        {
            this.HasKey(l => l.Code);

            this.Property(l => l.Code)
                .HasMaxLength(Config.MaxLength.Language.Code);

            this.Property(l => l.Description)
                .HasMaxLength(Config.MaxLength.Language.Description);
        }
    }
}
