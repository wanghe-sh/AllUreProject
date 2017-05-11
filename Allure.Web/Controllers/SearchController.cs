using Allure.Data;
using Allure.Core.Models;
using Allure.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Allure.Web.Controllers
{
    public partial class SearchController : MainControllerBase
    {
        public virtual ActionResult Index()
        {
            var model = new CategoryIndex();

            using (var dbContext = new AllureContext())
            {
                string searchKey = Request.QueryString["q"];

                var current = dbContext.Set<Category>().FirstOrDefault(c=>c.ParentId.HasValue );

                if (current == null)
                {
                    throw new HttpException(404, string.Format("category doesn't exist"));
                }

                model.Current = new SubCategoryOutput(current, this.LanguageCode);
                model.Current.SearchKey = searchKey;

                if (string.IsNullOrEmpty(searchKey))
                {
                    model.Products = dbContext.Set<Product>()
                    .Include(p => p.Locale)
                    .Include(p => p.Localizations)
                    .ToArray()
                    .Select(p => new ProductOutput(p, this.LanguageCode))
                    .ToArray();
                }
                else
                {
                    model.Products = dbContext.Set<Product>()
                    .Include(p => p.Locale)
                    .Include(p => p.Localizations)
                    .Where(p => p.Name.Contains(searchKey))
                    .ToArray()
                    .Select(p => new ProductOutput(p, this.LanguageCode))
                    .ToArray();
                }
                
            }

            return View(CreateViewModel(model));
        }

        public virtual ActionResult Products(string id)
        {
            using (var dbContext = new AllureContext())
            {
                var products = dbContext.Set<Product>()
                   .Include(p => p.Locale)
                   .Include(p => p.Localizations)
                   .Where(p => p.Name.Contains(id))
                   .ToArray()
                   .Select(p => new ProductOutput(p, this.LanguageCode))
                   .ToArray();

                return Json(products, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
