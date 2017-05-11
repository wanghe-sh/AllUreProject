using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class Locale
    {
        public Locale()
        {
            this.Localizations = new List<LocaleLocalization>();
        }

        public int Id { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<LocaleLocalization> Localizations { get; set; }
    }
}
