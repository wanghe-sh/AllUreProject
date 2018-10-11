using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class SubCategory
    {
        public SubCategory()
        {
            this.Localized = new List<LocalizedSubCategory>();
        }

        public int Id { get; set; }

        public int ParentId { get; set; }
        
        public string ImageUrl { get; set; }

        public virtual ICollection<LocalizedSubCategory> Localized { get; set; }

        public virtual Category Parent { get; set; }
    }
}
