using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class LocalizedImage : ILocalized
    {
        public int ImageId { get; set; }

        public string LanguageCode { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual Image Image { get; set; }

        public virtual Language Language { get; set; }
    }
}
