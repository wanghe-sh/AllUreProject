using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Criteria.SubscribedMails
{
    public class AddSubscribedMail
    {
        [Required]
        public string Mail { get; set; }
    }
}
