using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class LocalizedLocale : ILocalized
    {
        public int LocaleId { get; set; }

        public string LanguageCode { get; set; }

        public string Name { get; set; }

        public virtual Language Language { get; set; }

        public virtual Locale Locale { get; set; }
    }
}
