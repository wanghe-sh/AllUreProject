using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Web.Utility
{
    public class AllureUser : IPrincipal
    {
        public AllureUser(AllureIdentity identity)
        {
            this.Identity = identity;
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            AllureIdentity identity = this.Identity as AllureIdentity;
            if (identity != null)
            {
                return identity.User.Roles.Any(r => role.ToLower() == r.Role.ToString().ToLower());
            }

            return false;
        }
    }
}
