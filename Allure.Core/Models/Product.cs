using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class Product
    {
        public Product()
        {
            this.Images = new List<Image>();
            this.Localizations = new List<ProductLocalization>();
        }

        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public int LocaleId { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public decimal Price { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public string VideoUrl { get; set; }

        public bool RecommendedS { get; set; }

        public bool RecommendedL { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreateTime { get; set; }

        public bool Deleted { get; set; }
        
        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        public virtual Locale Locale { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ProductLocalization> Localizations { get; set; }

        public virtual ProductStorage Storage { get; set; }

        //new 
        public decimal OriginalPrice { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Length { get; set; }
        public string EnglishName { get; set; }
    }
}
