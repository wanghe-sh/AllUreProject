using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Models;

namespace Allure.Data.ModelConfigurations
{
    abstract class LocalizationConfiguration<T> : EntityTypeConfiguration<T>
        where T : class, ILocalized
    {
        public LocalizationConfiguration()
        {
            this.HasRequired<Language>(e => e.Language)
                .WithMany()
                .HasForeignKey(e => e.LanguageCode);           
        }
    }
}
