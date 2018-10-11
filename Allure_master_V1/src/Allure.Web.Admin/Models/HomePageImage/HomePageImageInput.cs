using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class HomePageImageInput
    {
        public string Id { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string NaviateUrl { get; set; }

        public LocalizedHomePageImageInput[] Localized { get; set; }
    }
}