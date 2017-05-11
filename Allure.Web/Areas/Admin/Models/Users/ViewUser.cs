using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;

namespace Allure.Web.Areas.Admin.Models.Users
{
    [MapFrom(typeof(User))]
    public class ViewUser
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public string Company { get; set; }

        public UserStatus Status { get; set; }

        public Role[] Roles { get; set; }

        public ViewUserDelivery[] Deliveries { get; set; }
    }

    [MapFrom(typeof(UserDelivery))]
    public class ViewUserDelivery
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public string Receiver { get; set; }

        public string Phone { get; set; }
    }
}