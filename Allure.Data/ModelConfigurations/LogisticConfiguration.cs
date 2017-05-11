using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace Allure.Data.ModelConfigurations
{
    class LogisticConfiguration : EntityTypeConfiguration<Logistic>
    {
        public LogisticConfiguration()
        {
            this.HasKey(l => l.Id);

            this.Property(l => l.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(l => l.Name)
                .HasMaxLength(Config.MaxLength.Logistic.Name);
        }
    }
}
