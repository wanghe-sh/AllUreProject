using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Criteria.CheckAddresses
{
    public class UpdateCheckAddress
    {
        [Required]
        [MaxLength(Config.MaxLength.CheckAddress.Address)]
        public string Address { get; set; }
    }
}
