using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Criteria.Logistics
{
    public class AddLogistic
    {
        [Required]
        [MaxLength(Config.MaxLength.Logistic.Name)]
        public string Name { get; set; }
    }
}
