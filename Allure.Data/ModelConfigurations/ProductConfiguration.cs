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

            this.Property(p => p.Number)
                .HasMaxLength(20)
                .IsRequired()
                .IsIndex(unique: true);

            this.HasRequired<Locale>(p => p.Locale)
                .WithMany()
                .HasForeignKey(p => p.LocaleId);

            this.HasRequired<Brand>(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId);

            this.HasRequired<Category>(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);

            this.HasMany<Image>(p => p.Images)
                .WithMany()
                .Map(m => m
                    .MapLeftKey(nameof(Product) + nameof(Product.Id))
                    .MapRightKey(nameof(Image) + nameof(Image.Id)));

            this.HasMany<ProductLocalization>(p => p.Localizations)
                .WithRequired()
                .HasForeignKey(l => l.ProductId);

            this.HasRequired<ProductStorage>(p => p.Storage)
                .WithRequiredPrincipal();
        }
    }

    class LocalizedProductConfiguration : LocalizationConfiguration<ProductLocalization>
    {
        public LocalizedProductConfiguration()
        {
            this.HasKey(p => new { p.ProductId, p.LanguageCode });
        }
    }

    class ProductStorageConfiguration : EntityTypeConfiguration<ProductStorage>
    {
        public ProductStorageConfiguration()
        {
            this.HasKey(s => s.ProductId);

            this.Property(s => s.RowVersion)
                .IsConcurrencyToken();
        }
    }
}