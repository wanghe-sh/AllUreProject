using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class Image
    {
        public Image()
        {
            this.Localized = new List<LocalizedImage>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string NavigationUrl { get; set; }

        public virtual ICollection<LocalizedImage> Localized { get; set; }
    }
}
