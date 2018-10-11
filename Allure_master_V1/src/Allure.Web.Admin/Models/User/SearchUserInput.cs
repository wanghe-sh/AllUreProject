using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class SearchUserInput
    {
        public string LastName { get; set; }

        public string Email { get; set; }

        public Gender? Gender { get; set; }

        public string Mobile { get; set; }

        public string Telephone { get; set; }

        public string Company { get; set; }

        public Role[] Roles { get; set; }

        public UserStatus? Status { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}