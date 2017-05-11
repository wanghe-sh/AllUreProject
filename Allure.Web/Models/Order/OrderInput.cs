using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Web.Models
{
    public class OrderInput
    {
        public int Id { get; set; }

        public bool WillCheck { get; set; }

        public string CheckAddress { get; set; }

        public DateTime? CheckTime { get; set; }

        public string CheckContact { get; set; }

        public string ReceiverName { get; set; }

        public string ReceiverContact { get; set; }

        public string ReceiverAddress { get; set; }

        public string ReceiverPostCode { get; set; }

        public OrderDetailInput[] Details { get; set; }
    }
}