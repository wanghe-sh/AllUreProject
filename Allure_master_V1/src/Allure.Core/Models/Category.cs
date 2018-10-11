using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class Category
    {
        public Category()
        {
            this.Children = new List<SubCategory>();
            this.Localized = new List<LocalizedCategory>();
        }

        public int Id { get; set; }

        public virtual ICollection<LocalizedCategory> Localized { get; set; }

        public virtual ICollection<SubCategory> Children { get; set; }
    }
}
