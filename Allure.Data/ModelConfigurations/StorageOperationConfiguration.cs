using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core;
using Allure.Core.Models;

namespace Allure.Data.ModelConfigurations
{
    class StorageOperationConfiguration : EntityTypeConfiguration<StorageOperation>
    {
        public StorageOperationConfiguration()
        {
            this.HasKey(o => o.Id);
            
            this.HasRequired<Logistic>(o => o.Logistic)
                .WithMany()
                .HasForeignKey(o => o.LogisticId);

            this.HasRequired<Warehouse>(o => o.Warehouse)
                .WithMany()
                .HasForeignKey(o => o.WarehouseId);

            this.HasOptional<Order>(o => o.Order)
                .WithMany()
                .HasForeignKey(o => o.OrderId);

            this.HasMany<StorageOperationDetail>(o => o.Details)
                .WithRequired()
                .HasForeignKey(d => d.StorageOperationId);

            this.HasRequired<User>(o => o.CreateBy)
                .WithMany()
                .HasForeignKey(o => o.CreatorId)
                .WillCascadeOnDelete(false);

            this.HasRequired<User>(o => o.UpdateBy)
                .WithMany()
                .HasForeignKey(o => o.UpdaterId)
                .WillCascadeOnDelete(false);
        }
    }

    class StorageOperationDetailConfiguration : EntityTypeConfiguration<StorageOperationDetail>
    {
        public StorageOperationDetailConfiguration()
        {
            this.HasKey(d => new { d.StorageOperationId, d.ProductId });

            this.HasRequired(d => d.Product)
                .WithMany()
                .HasForeignKey(d => d.ProductId);
        }
    }
}
