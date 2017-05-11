using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Allure.Common;
using System.Threading;
using System.Globalization;
using Allure.Data;
using Allure.Core.Models;
using System.Web.Security;
using Newtonsoft.Json;

namespace Allure.Web.Utility
{
    public class ContextModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.AuthenticateRequest += AuthenticateRequest;
        }

        void AuthenticateRequest(object sender, EventArgs e)
        {
            var identity = AllureIdentity.Anonymous;

            var application = (HttpApplication)sender;
            var context = application.Request.RequestContext.HttpContext;
            var cookie = application.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null)
            {
                var token = cookie.Value;
                var ticket = FormsAuthentication.Decrypt(token);                
                if (ticket != null && !ticket.Expired)
                {
                    var user = JsonConvert.DeserializeObject<User>(ticket.UserData);
                    identity = new AllureIdentity(user);
                }
            }

            context.User = new AllureUser(identity);
        }

        public void Dispose() { }
    }
}
