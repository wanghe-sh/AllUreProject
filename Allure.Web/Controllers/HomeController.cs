using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Allure.Core.Models;
using Allure.Data;
using Allure.Web.Models;

namespace Allure.Web.Controllers
{
    public class HomeController : MainControllerBase
    {
        public virtual ActionResult Index()
        {
            using (var context = new AllureContext())
            {
                var products = context
                    .Set<Product>()
                    .Include(p=>p.Localizations)
                    .Include(p=>p.Images)
                    //.Where(p => p.RecommendedL || p.RecommendedS)
                    .ToArray()
                    .Select(p => new ProductOutput(p, this.LanguageCode))
                    .ToArray();

                var model = CreateViewModel(products);

                return View(model);
            }
        }

        public virtual ActionResult About()
        {
            var model = CreateViewModel();

            return View(model);
        }
        
        public virtual ActionResult Terms()
        {
            var model = CreateViewModel();

            return View(model);
        }
        public virtual ActionResult Privacy()
        {
            var model = CreateViewModel();

            return View(model);
        }

        public virtual ActionResult Salespolicy()
        {
            var model = CreateViewModel();

            return View(model);
        }
        public virtual ActionResult Support()
        {
            var model = CreateViewModel();

            return View(model);
        }
    }
}