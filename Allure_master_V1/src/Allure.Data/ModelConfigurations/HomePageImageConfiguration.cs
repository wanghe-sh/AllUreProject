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
    class HomePageImageConfiguration : EntityTypeConfiguration<HomePageImage>
    {
        public HomePageImageConfiguration()
        {
            this.HasKey(i => i.Id);

            this.HasMany(i => i.Localized)
                .WithRequired()
                .HasForeignKey(l => l.HomePageImageId);
        }
    }

    class LocalizedHomePageImageConfiguration : LocalizedEntityConfiguration<LocalizedHomePageImage>
    {
        public LocalizedHomePageImageConfiguration()
        {
            this.HasKey(i => new { i.HomePageImageId, i.LanguageCode });
        }
    }
}
