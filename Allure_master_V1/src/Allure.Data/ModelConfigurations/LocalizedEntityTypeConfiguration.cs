using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Data.ModelConfigurations
{
    abstract class LocalizedEntityConfiguration<T> : EntityTypeConfiguration<T>
        where T : class, ILocalized
    {
        public LocalizedEntityConfiguration()
        {
            this.HasRequired<Language>(e => e.Language)
                .WithMany()
                .HasForeignKey(e => e.LanguageCode);           
        }
    }
}
