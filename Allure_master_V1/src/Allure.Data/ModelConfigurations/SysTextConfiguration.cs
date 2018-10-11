using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Data.ModelConfigurations
{
    class SysTextConfiguration : EntityTypeConfiguration<SysText>
    {
        public SysTextConfiguration()
        {
            this.HasKey(s => s.Code);

            this.Property(s => s.Code).HasMaxLength(20);
        }
    }

    class LocalizedSysTextConfiguration : LocalizedEntityConfiguration<LocalizedSysText>
    {
        public LocalizedSysTextConfiguration()
        {
            this.HasKey(s => new { s.SysTextCode, s.LanguageCode });
            
            this.HasRequired<SysText>(s => s.SysText)
                .WithMany()
                .HasForeignKey(s => s.SysTextCode);
        }
    }
}
