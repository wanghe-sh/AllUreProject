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

namespace Allure.Web.Utility
{   
    public class LanguageModule : IHttpModule
    {
        private static readonly string SpecifiedLanguageQueryString = "lang";
        private static readonly string SpecifiedLanguageHeader = "X-LanguageCode";
        private static readonly string SpecifiedLanguageCookieName = "lang";

        public void Init(HttpApplication app)        
        {
            app.BeginRequest += context_BeginRequest;
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            var dbContext = new AllureContext();
            var application = (HttpApplication)sender;
            string[] supported = dbContext.Set<Language>().Where(lang => lang.Enabled).Select(lang => lang.Code).ToArray();

            string querySpecified = application.Request.QueryString[SpecifiedLanguageQueryString];
            if (!querySpecified.IsNullOrEmpty() && supported.Contains(querySpecified))
            {
                SetCulture(application.Response, querySpecified);
                return;
            }

            string headerSpecified = application.Request.Headers[SpecifiedLanguageHeader];            
            if (!headerSpecified.IsNullOrEmpty() && supported.Contains(headerSpecified))
            {
                SetCulture(application.Response, headerSpecified);
                return;
            }

            var cookieSpecified = application.Request.Cookies[SpecifiedLanguageCookieName];
            if (cookieSpecified != null && supported.Contains(cookieSpecified.Value))
            {
                SetCulture(application.Response, cookieSpecified.Value);
                return;
            }

            string browserSpecified = application.Request.UserLanguages.FirstOrDefault(l => supported.Contains(l));
            SetCulture(application.Response, browserSpecified ?? dbContext.Set<Language>().Single(lang => lang.IsDefault).Code);
        }

        void SetCulture(HttpResponse response, string languageCode)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(languageCode);
            var cookie = new HttpCookie(SpecifiedLanguageCookieName, languageCode)
            {
                Expires = DateTime.Now.AddYears(1)                 
            };
            response.Cookies.Add(cookie);
        }

        public void Dispose() { }
    }
}
