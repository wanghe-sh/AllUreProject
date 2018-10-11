using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class UpdateUserInput
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string Company { get; set; }

        public UserStatus Status { get; set; }
        
        public Role[] Roles { get; set; }

        public UpdateDeliveryInput[] Deliveries { get; set; }
    }
}