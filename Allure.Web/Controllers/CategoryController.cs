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
    public partial class CategoryController : MainControllerBase
    {
        public virtual ActionResult Index(int id)
        {
            var model = new CategoryIndex();

            using (var dbContext = new AllureContext())
            {
                var current = dbContext.Set<Category>().SingleOrDefault(c => c.Id == id);

                if (current == null)
                {
                    throw new HttpException(404, string.Format("category {0} doesn't exist", id.ToString()));
                }

                model.Current = new SubCategoryOutput(current, this.LanguageCode);

                model.SubCategories = dbContext.Set<Category>()
                    .Include(c => c.Localizations)
                    .Where(c => c.ParentId == current.ParentId)
                    .ToArray()
                    .Select(c => new SubCategoryOutput(c, this.LanguageCode))
                    .ToArray();

                model.Products = dbContext.Set<Product>()
                    .Include(p => p.Locale)
                    .Include(p => p.Localizations)
                    .Where(p => p.CategoryId == current.Id)
                    .ToArray()
                    .Select(p => new ProductOutput(p, this.LanguageCode))
                    .ToArray();
            }

            return View(CreateViewModel(model));
        }

        public virtual ActionResult Products(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var products = dbContext.Set<Product>()
                   .Include(p => p.Locale)
                   .Include(p => p.Localizations)
                   .Where(p => p.CategoryId == id)
                   .ToArray()
                   .Select(p => new ProductOutput(p, this.LanguageCode))
                   .ToArray();

                return Json(products, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
