using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class ProductImageInput
    {
        public int Id { get; set; }

        public string ThumbnailUrl { get; set; }

        public string ImageUrl { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }
    }
}