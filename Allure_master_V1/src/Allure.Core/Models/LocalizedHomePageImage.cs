using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class LocalizedHomePageImage : ILocalized
    {
        public string HomePageImageId { get; set; }

        public string LanguageCode { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual HomePageImage HomePageImage { get; set; }

        public virtual Language Language { get; set; }
    }
}
