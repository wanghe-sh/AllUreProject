using Allure.Common;
using Allure.Data;
using Allure.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Allure.Core;
using Allure.Web.Models;

namespace Allure.Web.Controllers
{
    public partial class UserController : MainControllerBase
    {
        public readonly string ActivateSuccessUrl = "~/Views/User/ActivateSuccess.cshtml";
        public readonly string FindPasswordUrl = "~/Views/User/FindPassword.cshtml";
        public readonly string LoginUrl = "~/Views/User/Login.cshtml";
        public readonly string RegisterUrl = "~/Views/User/Register.cshtml";
        public readonly string RegisterSuccessUrl = "~/Views/User/RegisterSuccess.cshtml";
        public readonly string ResetPasswordUrl = "~/Views/User/ResetPassword.cshtml";

        [HttpGet]
        public virtual ActionResult Register()
        {
            var vm = CreateViewModel();
            vm.IsSimpleModel = true;
            return View(vm);
        }

        string prefix = "activate_";
        string prefix_reset = "reset_";
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
                Company = reg.Company??string.Empty,
                Gender = reg.Gender,
                Mobile = reg.Mobile,
                Password = reg.PlainTextPassword,
                PostCode = reg.PostCode,
                Telephone = reg.Telephone,
                Status = UserStatus.Unactivated,
                Roles = new[] { new UserRole { Role = Role.Customer } },
                Deliveries = new[] { new UserDelivery { Address = reg.Address ?? string.Empty, Phone = reg.Mobile??string.Empty, PostCode = reg.PostCode, Receiver = reg.FirstName + " " + reg.LastName } }
            };

            using (var context = new AllureContext())
            {
                context.Set<User>().Add(user);
                context.SaveChanges();
            }

            ViewBag.ActivateUrl = "/user/activate?token=" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(prefix + user.Email));

            string emailBody = string.Format(@"Dear {0} {1}, <br/>please click this link to activate your account. <a href=''{2}{3}''>{2}{3}</a>", user.FirstName, user.LastName, System.Configuration.ConfigurationManager.AppSettings["WebSite"], ViewBag.ActivateUrl);
            Utility.Email.SendEmail(user.Email, "Activate Account Of Allure Web Site", emailBody);

            var vm = CreateViewModel();
            vm.IsSimpleModel = true;
            return View(RegisterSuccessUrl, vm);
        }

        public virtual ActionResult Activate(string username, string token)
        {
            try
            {
                var decrypted = string.Empty;
                try
                {
                    decrypted = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token));
                }
                catch (Exception)
                {
                    //激活页面切换语言会丢失"="
                    if (!token.EndsWith("="))
                    {
                        token += "=";
                    }
                    decrypted = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token));
                }

                var valid = decrypted.StartsWith(prefix);

                if (valid)
                {
                    var email = decrypted.Substring(prefix.Length, decrypted.Length - prefix.Length);
                    using (var context = new AllureContext())
                    {
                        var user = context.Set<User>().Single(u => u.Email == email);
                        if (user != null)
                        {
                            user.Status = UserStatus.Normal;
                            context.SaveChanges();
                        }
                        else
                        {
                            return new HttpNotFoundResult();
                        }
                    }
                    var vm = CreateViewModel();
                    vm.IsSimpleModel = true;
                    return View(ActivateSuccessUrl, vm);
                }
                else
                {
                    return new HttpNotFoundResult();
                }
            }
            catch (Exception)
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
            ViewBag.LoginFail = false;
            return View(vm);
        }

        [HttpPost]
        public virtual ActionResult Login(UserLogin login)
        {
            var vm = CreateViewModel(login);
            vm.IsSimpleModel = true;
            ViewBag.LoginFail = false;
            ViewBag.ReturnUrl = string.Empty;
            var returnUrl = string.IsNullOrEmpty(login.ReturnUrl) ? "/" : HttpUtility.UrlDecode(login.ReturnUrl);

            ViewBag.ReturnUrl = returnUrl;

            using (var context = new AllureContext())
            {
                var user = context.Set<User>().SingleOrDefault(u => u.Email == login.Email && u.Password == login.PlainTextPassword);

                if (user != null)
                {
                    if (user.Status == UserStatus.Normal)
                    {
                        user.Deliveries = user.Deliveries.Take(3).ToList();

                        var userData = JsonConvert.SerializeObject(user);
                        var ticket = new FormsAuthenticationTicket(0, FormsAuthentication.FormsCookieName, DateTime.Now,
                            DateTime.Now.AddMinutes(60), false, userData);
                        var token = FormsAuthentication.Encrypt(ticket);
                        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, token));
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        ViewBag.LoginFail = true;
                    }
                }
            }

            return View(vm);
        }

        [HttpPost]
        public virtual ActionResult FindPassword(UserRegister reg)
        {
            bool emailExists = false;
            if (!string.IsNullOrEmpty(reg.Email))
            {
                using (var context = new AllureContext())
                {
                    var user = context.Set<User>().FirstOrDefault(u => u.Email == reg.Email);
                    if (user != null)
                    {
                        emailExists = true;
                        string activateUrl = "/user/resetpassword?token=" + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(prefix_reset + user.Email));
                        
                        string emailBody =
                            string.Format(@"Dear {0} {1}, <br/>please click this link to reset your password. <a href=''{2}{3}''>{2}{3}</a>",
                                user.FirstName, user.LastName,
                                System.Configuration.ConfigurationManager.AppSettings["WebSite"], activateUrl);
                        Utility.Email.SendEmail(user.Email, "Reset Password Of Allure Web Site", emailBody);

                    }
                }
            }

            var vm = CreateViewModel();
            vm.IsSimpleModel = true;
            ViewBag.IsFindPassword = emailExists;

            return View(vm);
        }

        public virtual ActionResult FindPassword()
        {
            var vm = CreateViewModel();
            vm.IsSimpleModel = true;
            ViewBag.IsFindPassword = false;
            return View(vm);
        }

        [HttpGet]
        public virtual ActionResult ResetPassword(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return new HttpNotFoundResult();
            }

            ViewBag.IsResetPassword = false;

            string decrypted = string.Empty;
            try
            {
                decrypted = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token));
            }
            catch
            {
            }

            if (string.IsNullOrEmpty(decrypted))
            {
                //激活页面切换语言会丢失"="
                if (!token.EndsWith("="))
                {
                    token += "=";
                }
            }
            
            try
            {
                decrypted = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var valid = decrypted.StartsWith(prefix_reset);

                if (valid)
                {
                    var email = decrypted.Substring(prefix_reset.Length, decrypted.Length - prefix_reset.Length);
                    using (var context = new AllureContext())
                    {
                        var user = context.Set<User>().Single(u => u.Email == email);
                        if (user == null)
                        {
                            return new HttpNotFoundResult();
                        }
                    }
                    ViewBag.Email = email;
                    var vm = CreateViewModel();
                    vm.IsSimpleModel = true;
                    return View(vm);
                }
                else
                {
                    return new HttpNotFoundResult();
                }
            }
            catch (Exception)
            {
                return new HttpNotFoundResult();
            }

        }

        [HttpPost]
        public virtual ActionResult ResetPassword(string email, string userPassword)
        {
            ViewBag.IsResetPassword = false;

            if (!string.IsNullOrWhiteSpace(email))
            {
                using (var context = new AllureContext())
                {
                    var user = context.Set<User>().Single(u => u.Email == email);
                    if (user != null)
                    {
                        user.Password = userPassword;
                        context.SaveChanges();

                        ViewBag.IsResetPassword = true;
                    }
                }
            }
            var vm = CreateViewModel();
            vm.IsSimpleModel = true;
            return View(vm);
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
