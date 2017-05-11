using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Models;

namespace Allure.Data.ModelConfigurations
{
    class LocaleConfiguration : EntityTypeConfiguration<Locale>
    {
        public LocaleConfiguration()
        {
            this.HasKey(l => l.Id);

            this.HasMany<LocaleLocalization>(l => l.Localizations)
                .WithRequired()
                .HasForeignKey(ll => ll.LocaleId);
        }
    }

    class LocaleLocalizationConfiguration : LocalizationConfiguration<LocaleLocalization>
    {
        public LocaleLocalizationConfiguration()
        {
            this.HasKey(l => new { l.LocaleId, l.LanguageCode });
        }
    }
}
