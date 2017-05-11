using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Allure.Web.Areas.Admin.Controllers
{
    [Authorize(Roles =("CustomerService,WareHouse,WarehouseAdmin,Logistics,SystemAdmin"))]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}