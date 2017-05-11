using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common.Validations;

namespace Allure.Core.Criteria.Products
{
    public class AddProduct
    {
        [Required]
        public int? CategoryId { get; set; }

        [Required]
        public int? BrandId { get; set; }

        [Required]
        public int? LocaleId { get; set; }
                
        public int[] ImageIds { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(Config.MaxLength.Product.Name)]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(Config.MaxLength.Product.Number)]
        public string Number { get; set; }

        [Required]
        public decimal? Price { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public TimeSpan? DeliveryTerm { get; set; }

        public string VideoUrl { get; set; }
                
        public bool RecommendedS { get; set; }
        
        public bool RecommendedL { get; set; }
        
        [ValidateChildren]
        public Localization[] Localizations { get; set; }
        
        public int DisplayOrder { get; set; }
    }
}
