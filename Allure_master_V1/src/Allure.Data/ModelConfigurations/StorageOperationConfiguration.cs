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
    class StorageOperationConfiguration : EntityTypeConfiguration<StorageOperation>
    {
        public StorageOperationConfiguration()
        {
            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasOptional(s => s.Order)
                .WithMany()
                .HasForeignKey(s => s.OrderId);

            this.HasRequired(s => s.Logistic)
                .WithMany()
                .HasForeignKey(s => s.LogisticCode);

            this.HasRequired(s => s.Warehouse)
                .WithMany()
                .HasForeignKey(s => s.WarehouseCode);

            this.HasMany(s => s.Details)
                .WithRequired()
                .HasForeignKey(d => d.StorageOperationId);
        }
    }

    class StorageOperationDetailConfiguration : EntityTypeConfiguration<StorageOperationDetail>
    {
        public StorageOperationDetailConfiguration()
        {
            this.HasKey(d => d.Id);
            this.Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(d => d.Product)
                .WithMany()
                .HasForeignKey(d => d.ProductId);
        }
    }
}
