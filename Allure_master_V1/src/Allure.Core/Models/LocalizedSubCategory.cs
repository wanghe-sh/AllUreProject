using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class LocalizedSubCategory : ILocalized
    {
        public int SubCategoryId { get; set; }

        public string LanguageCode { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual Language Language { get; set; }
    }
}
