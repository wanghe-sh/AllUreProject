using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Data.ModelConfigurations
{
    class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            this.HasKey(o => o.Id);

            this.HasRequired<User>(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId);

            this.HasMany<OrderDetail>(o => o.Details)
                .WithRequired()
                .HasForeignKey(d => d.OrderId);
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
