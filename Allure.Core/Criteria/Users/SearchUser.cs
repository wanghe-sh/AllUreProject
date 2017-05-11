using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Models;

namespace Allure.Core.Criteria.Users
{
    public class SearchUser
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        public string Company { get; set; }

        public UserStatus? Status { get; set; }

        public Role[] Roles { get; set; }

        public SortUserBy? OrderBy { get; set; }

        public bool Descending { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }

    public enum SortUserBy
    {
        Status
    }
}
