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
    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.HasKey(u => u.Id);

            this.Property(u => u.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(u => u.Email)
                .HasMaxLength(Config.MaxLength.User.Email)
                .IsRequired();

            this.Property(u => u.Password)
                .HasMaxLength(100)
                .IsRequired();

            this.Property(u => u.FirstName)
                .HasMaxLength(Config.MaxLength.User.FirstName);

            this.Property(u => u.LastName)
                .HasMaxLength(Config.MaxLength.User.LastName)
                .IsRequired();

            this.Property(u => u.Telephone)
                .HasMaxLength(Config.MaxLength.User.Telephone);

            this.Property(u => u.Mobile)
                .HasMaxLength(Config.MaxLength.User.Mobile);

            this.Property(u => u.Address)
                .HasMaxLength(Config.MaxLength.User.Address);

            this.Property(u => u.PostCode)
                .HasMaxLength(Config.MaxLength.User.PostCode);

            this.Property(u => u.Company)
                .HasMaxLength(Config.MaxLength.User.Company)
                .IsRequired();

            this.HasMany<UserRole>(u => u.Roles)
                .WithRequired()
                .HasForeignKey(r => r.UserId)
                .WillCascadeOnDelete(true);

            this.HasMany<UserDelivery>(u => u.Deliveries)
                .WithRequired()
                .HasForeignKey(d => d.UserId)
                .WillCascadeOnDelete(false);              
        }
    }

    class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            this.HasKey(r => new { r.UserId, r.Role });
        }
    }

    class UserDeliveryConfiguration : EntityTypeConfiguration<UserDelivery>
    {
        public UserDeliveryConfiguration()
        {
            this.HasKey(d => d.Id);

            this.Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(d => d.UserId).IsIndex();

            this.Property(d => d.Address)
                .HasMaxLength(Config.MaxLength.User.Delivery.Address)
                .IsRequired();

            this.Property(d => d.PostCode)
                .HasMaxLength(Config.MaxLength.User.Delivery.PostCode);

            this.Property(d => d.Receiver)
                .HasMaxLength(Config.MaxLength.User.Delivery.Receiver)
                .IsRequired();

            this.Property(d => d.Phone)
                .HasMaxLength(Config.MaxLength.User.Delivery.Phone)
                .IsRequired();
        }
    }
}
