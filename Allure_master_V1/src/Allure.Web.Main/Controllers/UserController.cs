using Allure.Common;
using Allure.Data;
using Allure.Core.Models;
using Allure.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Allure.UI.Controllers
{
    public partial class UserController : MainControllerBase
    {
        [HttpGet]
        public virtual ActionResult Register()
        {
            var vm = CreateViewModel();
            vm.IsSimpleModel = true;
            return View(vm);
        }

        string prefix = "activate_";
        byte[] key = System.Text.Encoding.UTF8.GetBytes("thekey");
        byte[] iv = System.Text.Encoding.UTF8.GetBytes("iv");

        [HttpPost]
        public virtual ActionResult Register(UserRegister reg)
        {
            var user = new User
            {
                Email = reg.Email,
                FirstName = reg.FirstName,
                LastName = reg.LastName,
                Address = reg.Address,
                Company = reg.Company,
                Gender = reg.Gender,
                Mobile = reg.Mobile,
                Password = reg.PlainTextPassword,
                PostCode = reg.PostCode,
                Telephone = reg.Telephone,
                Status = UserStatus.Unactivated,
                Roles = new[] { new UserRole { Role = Role.Customer } },
                Deliveries = new[] { new Delivery { Address = reg.Address, Phone = reg.Mobile, PostCode = reg.PostCode, Receiver = reg.FirstName + " " + reg.LastName } }
            };

            using (var context = new AllureContext())
            {
                context.Set<User>().Add(user);
                context.SaveChanges();
            }

            ViewBag.ActivateUrl = "/user/activate?token=" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(prefix + user.Email));

            return View(MVC.User.Views.RegisterSuccess, CreateViewModel());
        }

        public virtual ActionResult Activate(string username, string token)
        {
            var decrypted = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var valid = decrypted.StartsWith(prefix);

            if (valid)
            {
                var email = decrypted.Substring(prefix.Length, decrypted.Length - prefix.Length);
                using (var context = new AllureContext())
                {
                    var user = context.Set<User>().Single(u => u.Email == email);
                    user.Status = UserStatus.Normal;
                    context.SaveChanges();
                }

                return View(MVC.User.Views.ActivateSuccess, CreateViewModel());
            }
            else
            {
                return new HttpNotFoundResult();
            }
        }

        [HttpGet]
        public virtual ActionResult Login(string returnUrl)
        {
            var login = new UserLogin { ReturnUrl = returnUrl };
            var vm = CreateViewModel(login);
            vm.IsSimpleModel = true;
            return View(vm);
        }

        [HttpPost]
        public virtual ActionResult Login(UserLogin login)
        {
            var returnUrl = string.IsNullOrEmpty(login.ReturnUrl) ? "/" : login.ReturnUrl;

            using (var context = new AllureContext())
            {
                var user = context.Set<User>().SingleOrDefault(u => u.Email == login.Email && u.Password == login.PlainTextPassword);

                if (user != null)
                {
                    var userData = JsonConvert.SerializeObject(user);
                    var ticket = new FormsAuthenticationTicket(0, FormsAuthentication.FormsCookieName, DateTime.Now, DateTime.Now.AddMinutes(60), false, userData);                    
                    var token = FormsAuthentication.Encrypt(ticket);
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, token));
                    return Redirect(returnUrl);
                }
            }

            return View(CreateViewModel(login));
        }

        public virtual ActionResult FindPassword()
        {
            var vm = CreateViewModel();
            vm.IsSimpleModel = true;
            return View(vm);
        }

        [HttpGet]
        public virtual ActionResult ResetPassword()
        {
            var vm = CreateViewModel();
            vm.IsSimpleModel = true;
            return View(vm);
        }

        [HttpPost]
        public virtual ActionResult ResetPassword(string oldPassword, string newPassword)
        {
            return Content(string.Empty);
        }

        public virtual ActionResult EmailExists(string email)
        {
            var valid = false;

            if (!string.IsNullOrEmpty(email))
            {
                using (var context = new AllureContext())
                {
                    valid = !context.Set<User>().Any(u => u.Email == email);
                }
            }

            return Content(valid.ToString().ToLowerInvariant());
        }

        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Response.Cookies.Remove("user");
            return Redirect("/");
        }
    }
}
