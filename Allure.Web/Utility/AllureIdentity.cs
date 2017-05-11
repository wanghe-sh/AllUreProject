using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Web.Utility
{
    public class AllureIdentity : IIdentity
    {
        public static AllureIdentity Anonymous = new AllureIdentity(null);

        public AllureIdentity(User user)
        {
            this.User = user;
        }

        public string AuthenticationType
        {
            get { return "Allure"; }
        }

        public User User { get; private set; }

        public bool IsAuthenticated
        {
            get { return this.User != null; }
        }

        public string Name
        {
            get { return this.User == null ? string.Empty : this.User.Id.ToString(); }
        }
    }
}
