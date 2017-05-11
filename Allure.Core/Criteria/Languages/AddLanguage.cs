using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Criteria.Languages
{
    public class AddLanguage
    {
        [Required]
        [MaxLength(Config.MaxLength.Language.Code)]
        public string Code { get; set; }

        [Required]
        [MaxLength(Config.MaxLength.Language.Description)]
        public string Description { get; set; }

        public bool IsDefault { get; set; }

        public bool Enabled { get; set; }
    }
}
