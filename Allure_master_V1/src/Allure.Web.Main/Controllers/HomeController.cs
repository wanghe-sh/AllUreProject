using Allure.Core.Models;
using Allure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Allure.UI.Models;

namespace Allure.UI.Controllers
{
    public partial class HomeController : MainControllerBase
    {
        public virtual ActionResult Index()
        {
            using (var context = new AllureContext())
            {
                var products = context
                    .Set<Product>()
                    .Include(p => p.Localized)
                    .Include(p => p.Images)
                    .Where(p => p.RecommendedL || p.RecommendedS)
                    .ToArray()
                    .Select(p => new ProductOutput(p, this.LanguageCode))
                    .ToArray();

                var model = CreateViewModel(products);

                return View(model);
            }
        }
    }
}
