using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class SubscribedMail
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsValid { get; set; }
    }
}
