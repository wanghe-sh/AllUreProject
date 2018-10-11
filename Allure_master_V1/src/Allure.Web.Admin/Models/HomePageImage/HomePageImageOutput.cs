using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class HomePageImageOutput
    {
        public HomePageImageOutput(HomePageImage image)
        {
            this.Id = image.Id;
            this.Width = image.Width;
            this.Height = image.Height;
            this.ImageUrl = image.ImageUrl;
            this.NaviateUrl = image.NavigateUrl;
            this.Localized = image.Localized.Select(l => new LocalizedHomePageImageOutput(l)).ToArray();
        }

        public string Id { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string ImageUrl { get; set; }

        public string NaviateUrl { get; set; }

        public LocalizedHomePageImageOutput[] Localized { get; set; }
    }
}