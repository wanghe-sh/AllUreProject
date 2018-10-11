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
    class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            this.HasKey(c => c.Id);

            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasMany<SubCategory>(c => c.Children)
                .WithRequired()
                .HasForeignKey(sc => sc.ParentId);

            this.HasMany<LocalizedCategory>(c => c.Localized)
                .WithRequired()
                .HasForeignKey(lc => lc.CategoryId);
        }
    }

    class LocalizedCategoryConfiguration : LocalizedEntityConfiguration<LocalizedCategory>
    {
        public LocalizedCategoryConfiguration()
        {
            this.HasKey(c => c.Id);

            this.HasRequired<Category>(c => c.Category)
                .WithMany()
                .HasForeignKey(c => c.CategoryId);

            this.HasRequired<Language>(c => c.Language)
                .WithMany()
                .HasForeignKey(c => c.LanguageCode);
        }
    }
}
