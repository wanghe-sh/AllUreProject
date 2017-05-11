using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Criteria.Images
{
    public class AddImage
    {
        [Required]
        public int? Width { get; set; }

        [Required]
        public int? Height { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(Config.MaxLength.Image.Url)]
        public string Url { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(Config.MaxLength.Image.ThumbnailUrl)]
        public string ThumbnailUrl { get; set; }
    }
}
