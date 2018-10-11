using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class UserOutput
    {
        public UserOutput(User user)
        {
            UserId = user.Id;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Gender = user.Gender;
            Mobile = user.Mobile;
            Telephone = user.Telephone;
            Company = user.Company;
            Status = user.Status;
            Roles = user.Roles.Select(r => r.Role).ToArray();
            Deliveries = user.Deliveries.Select(d => new DeliveryOutput(d)).ToArray();
        }

        public int UserId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string Mobile { get; set; }

        public string Telephone { get; set; }

        public string Company { get; set; }
        
        public UserStatus Status { get; set; }

        public Role[] Roles { get; set; }

        public DeliveryOutput[] Deliveries { get; set; }
    }
}