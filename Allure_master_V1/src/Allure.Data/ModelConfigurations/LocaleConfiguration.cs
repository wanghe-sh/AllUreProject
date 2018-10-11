using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Data.ModelConfigurations
{
    class LocaleConfiguration : EntityTypeConfiguration<Locale>
    {
        public LocaleConfiguration()
        {
            this.HasKey(l => l.Id);

            this.HasMany<LocalizedLocale>(l => l.Localized)
                .WithRequired()
                .HasForeignKey(ll => ll.LocaleId);
        }
    }

    class LocalizedLocaleConfiguration : LocalizedEntityConfiguration<LocalizedLocale>
    {
        public LocalizedLocaleConfiguration()
        {
            this.HasKey(l => new { l.LocaleId, l.LanguageCode });
        }
    }
}
