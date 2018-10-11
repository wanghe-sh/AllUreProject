using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class HomePageImage
    {
        public HomePageImage()
        {
            this.Localized = new List<LocalizedHomePageImage>();
        }

        public string Id { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string ImageUrl { get; set; }

        public string NavigateUrl { get; set; }

        public virtual ICollection<LocalizedHomePageImage> Localized { get; set; }
    }
}
