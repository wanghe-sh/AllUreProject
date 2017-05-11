using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Criteria
{
    public class Localization
    {
        [Required]
        [MaxLength(Config.MaxLength.Language.Code)]
        public string LanguageCode { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public string DeliveryTerm { get; set; }
    }
}
