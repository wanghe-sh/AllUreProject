using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.UI.Models
{
    public class UserLogin
    {
        public string Email { get; set; }

        public string PlainTextPassword { get; set; }

        public string ReturnUrl { get; set; }

        public TimeSpan? RememberMe { get; set; }
    }
}