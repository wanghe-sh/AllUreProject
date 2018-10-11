using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class UpdateDeliveryInput
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public string Receiver { get; set; }

        public string Phone { get; set; }
    }
}