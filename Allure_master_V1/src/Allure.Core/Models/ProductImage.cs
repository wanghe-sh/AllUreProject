using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class ProductImage
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ThumbnailUrl { get; set; }

        public string ImageUrl { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public bool IsLargeImage { get { return this.Height > 500 && this.Width > 500; } }
    }
}
