using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class UserDelivery
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public string Receiver { get; set; }

        public string Phone { get; set; }
    }
}
