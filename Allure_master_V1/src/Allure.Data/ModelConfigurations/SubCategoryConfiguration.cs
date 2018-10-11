using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Data.ModelConfigurations
{
    class SubCategoryConfiguration : EntityTypeConfiguration<SubCategory>
    {
        public SubCategoryConfiguration()
        {
            this.HasKey(c => c.Id);

            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired<Category>(c => c.Parent)
                .WithMany()
                .HasForeignKey(sc => sc.ParentId);

            this.HasMany<LocalizedSubCategory>(sc => sc.Localized)
                .WithRequired()
                .HasForeignKey(lc => lc.SubCategoryId);
        }
    }

    class LocalizedSubCategoryConfiguration : LocalizedEntityConfiguration<LocalizedSubCategory>
    {
        public LocalizedSubCategoryConfiguration()
        {
            this.HasKey(c => new { c.SubCategoryId, c.LanguageCode });

            this.HasRequired<SubCategory>(c => c.SubCategory)
                .WithMany()
                .HasForeignKey(c => c.SubCategoryId);

            this.HasRequired<Language>(c => c.Language)
                .WithMany()
                .HasForeignKey(c => c.LanguageCode);
        }
    }
}
