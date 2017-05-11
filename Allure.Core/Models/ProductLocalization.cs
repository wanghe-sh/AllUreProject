using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class ProductLocalization : ILocalized
    {
        public int ProductId { get; set; }

        public string LanguageCode { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public string DeliveryTerm { get; set; }

        public virtual Language Language { get; set; }
    }
}
