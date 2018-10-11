using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class DeliveryOutput
    {
        public DeliveryOutput(Delivery delivery)
        {
            this.Id = delivery.Id;
            this.Address = delivery.Address;
            this.PostCode = delivery.PostCode;
            this.Receiver = delivery.Receiver;
            this.Phone = delivery.Phone;
        }

        public int Id { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public string Receiver { get; set; }

        public string Phone { get; set; }
    }
}