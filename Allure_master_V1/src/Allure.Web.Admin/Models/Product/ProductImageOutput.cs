using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class ProductImageOutput
    {
        public ProductImageOutput(ProductImage productImage)
        {
            this.Id = productImage.Id;
            this.ThumbnailUrl = productImage.ThumbnailUrl;
            this.ImageUrl = productImage.ImageUrl;
            this.Height = productImage.Height;
            this.Width = productImage.Width;         
        }

        public int Id { get; set; }

        public string ThumbnailUrl { get; set; }

        public string ImageUrl { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }
    }
}