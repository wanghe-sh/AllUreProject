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
    class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(p => p.Locale)
                .WithMany()
                .HasForeignKey(p => p.LocaleId);

            this.HasRequired(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId);

            this.HasRequired(p => p.SubCategory)
                .WithMany()
                .HasForeignKey(p => p.SubCategoryId);

            this.HasMany(p => p.Images)
                .WithRequired()
                .HasForeignKey(i => i.ProductId);

            this.HasMany(p => p.Localized)
                .WithRequired()
                .HasForeignKey(l => l.ProductId);

            this.HasRequired(p => p.Storage)
                .WithRequiredPrincipal()
                .WillCascadeOnDelete(true);
        }
    }

    class LocalizedProductConfiguration : LocalizedEntityConfiguration<LocalizedProduct>
    {
        public LocalizedProductConfiguration()
        {
            this.HasKey(p => p.Id);
        }
    }

    class ProductImageConfiguration : EntityTypeConfiguration<ProductImage>
    {
        public ProductImageConfiguration()
        {
            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    class ProductStorageConfiguration : EntityTypeConfiguration<ProductStorage>
    {
        public ProductStorageConfiguration()
        {
            this.HasKey(p => p.ProductId);
        }
    }
}
