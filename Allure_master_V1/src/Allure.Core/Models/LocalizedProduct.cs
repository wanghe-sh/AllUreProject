using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class LocalizedProduct : ILocalized
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string LanguageCode { get; set; }

        public string Description { get; set; }

        public virtual Product Product { get; set; }

        public virtual Language Language { get; set; }
    }
}
