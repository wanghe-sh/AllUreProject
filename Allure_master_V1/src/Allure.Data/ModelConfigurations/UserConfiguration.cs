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
    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.HasKey(u => u.Id);

            this.Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(u => u.Email).HasMaxLength(20).IsRequired();
            this.Property(u => u.Password);
            this.Property(u => u.FirstName).HasMaxLength(20);
            this.Property(u => u.LastName).HasMaxLength(20);
            this.Property(u => u.Telephone).HasMaxLength(20);
            this.Property(u => u.Mobile).HasMaxLength(20);
            this.Property(u => u.Address).HasMaxLength(100);
            this.Property(u => u.PostCode).HasMaxLength(20);
            this.Property(u => u.Company).HasMaxLength(50);

            this.HasMany<UserRole>(u => u.Roles)
                .WithRequired()
                .HasForeignKey(r => r.UserId)
                .WillCascadeOnDelete(true);

            this.HasMany<Delivery>(u => u.Deliveries)
                .WithRequired()
                .HasForeignKey(d => d.UserId)
                .WillCascadeOnDelete(true);
        }
    }

    class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            this.HasKey(r => new { r.UserId, r.Role });
        }
    }

    class DeliveryConfiguration : EntityTypeConfiguration<Delivery>
    {
        public DeliveryConfiguration()
        {
            this.HasKey(d => d.Id);

            this.Property(d => d.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(d => d.Address).HasMaxLength(100);
            this.Property(d => d.PostCode).HasMaxLength(20);
        }
    }
}
