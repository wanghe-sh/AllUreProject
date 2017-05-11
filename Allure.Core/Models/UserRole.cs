using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class UserRole
    {
        public int UserId { get; set; }

        public Role Role { get; set; }
    }
}
