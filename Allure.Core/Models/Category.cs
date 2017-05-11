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
            this.Localizations = new List<CategoryLocalization>();
            this.Children = new List<Category>();
        }

        public int Id { get; set; }

        public int? ParentId { get; set; }

        public int? ImageId { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<CategoryLocalization> Localizations { get; set; }

        public virtual ICollection<Category> Children { get; set; }

        public virtual Image Image { get; set; }
    }
}
