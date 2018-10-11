using Allure.Data;
using Allure.Core.Models;
using Allure.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Allure.UI.Controllers
{
    public partial class ProductController : MainControllerBase
    {
        public virtual ActionResult Index(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var product = dbContext
                    .Set<Product>()
                    .Include(p => p.Localized)
                    .SingleOrDefault(c => c.Id == id);

                if (product == null)
                {
                    throw new HttpException(404, string.Format("product {0} doesn't exist", id.ToString()));
                }

                var model = new ProductOutput(product, this.LanguageCode);
                return View(CreateViewModel(model));
            }
        }
    }
}
