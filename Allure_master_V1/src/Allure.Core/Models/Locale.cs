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
            this.Localized = new List<LocalizedLocale>();
        }

        public int Id { get; set; }

        public DataStatus Status { get; set; }

        public virtual ICollection<LocalizedLocale> Localized { get; set; }
    }
}
