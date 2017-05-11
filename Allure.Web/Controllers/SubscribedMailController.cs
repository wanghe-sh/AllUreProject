using Allure.Core.Models;
using Allure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Allure.Web.Controllers
{
    public class SubscribedMailController : Controller
    {
        // GET: SubscribedMail
        public JsonResult Index(string mail)
        {
            //return json
            var jsonObj = new { result = "success", msg = "" };

            if (string.IsNullOrWhiteSpace(mail))
            {
                jsonObj = new { result = "fail", msg = "" };
            }
            try
            {
                //save mail to database
                using (var context = new AllureContext())
                {
                    var subscripedMail = new SubscribedMail
                    {
                        Mail = mail,
                        CreateTime = DateTime.Now,
                        IsValid = true
                    };

                    context.Set<SubscribedMail>().Add(subscripedMail);
                    context.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                jsonObj = new { result = "fail", msg = ex.Message };
            }

            return Json(jsonObj);
        }
    }
}