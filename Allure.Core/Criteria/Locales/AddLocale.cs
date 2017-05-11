using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common.Validations;

namespace Allure.Core.Criteria.Locales
{
    public class AddLocale
    {
        [Required]
        [MinLength(1)]
        [ValidateChildren]
        public Localization[] Localizations { get; set; }
    }
}
