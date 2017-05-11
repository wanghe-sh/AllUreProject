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
    class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            this.HasKey(o => o.Id);

            this.Property(o => o.CustomerId)
                .IsIndex();

            this.Property(o => o.OrderNO)
                .HasMaxLength(Config.MaxLength.Order.OrderNO);

            this.Property(o => o.CheckAddress)
                .HasMaxLength(Config.MaxLength.Order.CheckAddress);

            this.Property(o => o.CheckContact)
                .HasMaxLength(Config.MaxLength.Order.CheckContact);

            this.HasRequired<User>(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId);

            this.HasRequired<UserDelivery>(o => o.Delivery)
                .WithMany()
                .HasForeignKey(o => o.DeliveryId);

            this.HasMany<OrderDetail>(o => o.Details)
                .WithRequired()
                .HasForeignKey(d => d.OrderId);

            this.HasOptional<Logistic>(o => o.Logistic)
                .WithMany()
                .HasForeignKey(o => o.LogisticId);
        }
    }

    class OrderDetailConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfiguration()
        {
            this.HasKey(d => d.Id);

            this.HasRequired(d => d.Product)
                .WithMany()
                .HasForeignKey(d => d.ProductId);
        }
    }
}
