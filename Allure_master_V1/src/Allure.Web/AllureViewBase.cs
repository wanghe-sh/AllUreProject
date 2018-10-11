using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Allure.Web
{
    public abstract class AllureViewBase<TModel> : WebViewPage<TModel>
    {
        public User CurrentUser
        {
            get
            {
                var identity = this.User.Identity as AllureIdentity;
                return identity.User;
            }
        }

        protected string AdminSite
        {
            get { return ConfigurationManager.AppSettings["AdminSite"]; }
        }
    }
}
