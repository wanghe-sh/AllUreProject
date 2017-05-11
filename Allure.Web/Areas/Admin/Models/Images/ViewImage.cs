using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;

namespace Allure.Web.Areas.Admin.Models.Images
{
    [MapFrom(typeof(Image))]
    public class ViewImage
    {
        public int Id { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Url { get; set; }

        public string ThumbnailUrl { get; set; }
    }
}