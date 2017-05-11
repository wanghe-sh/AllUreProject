using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace Allure.Data.ModelConfigurations
{
    class ImageConfiguration : EntityTypeConfiguration<Image>
    {
        public ImageConfiguration()
        {
            this.HasKey(i => i.Id);

            this.Property(i => i.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(i => i.Url)
                .HasMaxLength(Config.MaxLength.Image.Url);

            this.Property(i => i.ThumbnailUrl)
                .HasMaxLength(Config.MaxLength.Image.ThumbnailUrl);
        }
    }
}
