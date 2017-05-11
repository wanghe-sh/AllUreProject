using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Criteria.Brands
{
    public class UpdateBrand
    {
        [Required]
        [MaxLength(Config.MaxLength.Brand.Name)]
        public string Name { get; set; }
    }
}
