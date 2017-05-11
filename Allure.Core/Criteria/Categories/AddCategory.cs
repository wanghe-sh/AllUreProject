using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common.Validations;

namespace Allure.Core.Criteria.Categories
{
    public class AddCategory
    {
        public int? ParentId { get; set; }

        public int? ImageId { get; set; }

        [Required]
        [MinLength(1)]
        [ValidateChildren]
        public Localization[] Localizations { get; set; }
    }
}
