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
            this.Images = new List<ProductImage>();
            this.Localized = new List<LocalizedProduct>();
        }

        public int Id { get; set; }

        public int SubCategoryId { get; set; }

        public int BrandId { get; set; }

        public int LocaleId { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public decimal Price { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }
                
        public string VideoUrl { get; set; }

        public int DisplayOrder { get; set; }

        public bool RecommendedS { get; set; }

        public bool RecommendedL { get; set; }

        public DateTime CreateDate { get; set; }

        public DataStatus Status { get; set; }

        public ICollection<LocalizedProduct> Localized { get; set; }
        
        public virtual Brand Brand { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual Locale Locale { get; set; }

        public virtual ICollection<ProductImage> Images { get; set; }

        public virtual ProductStorage Storage { get; set; }
    }
}
