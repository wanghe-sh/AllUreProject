using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Models;

namespace Allure.Data.ModelConfigurations
{
    class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            this.HasKey(c => c.Id);

            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            this.HasMany<Category>(c => c.Children)
                .WithOptional()
                .HasForeignKey(c => c.ParentId);

            this.HasOptional<Image>(c => c.Image)
                .WithMany()
                .HasForeignKey(c => c.ImageId);

            this.HasMany<CategoryLocalization>(c => c.Localizations)
                .WithRequired()
                .HasForeignKey(l => l.CategoryId);
        }
    }

    class CategoryLocalizationConfiguration : LocalizationConfiguration<CategoryLocalization>
    {
        public CategoryLocalizationConfiguration()
        {
            this.HasKey(l => new { l.CategoryId, l.LanguageCode });
        }
    }
}
