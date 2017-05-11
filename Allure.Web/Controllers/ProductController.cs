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
    public partial class ProductController : MainControllerBase
    {
        public virtual ActionResult Index(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var product = dbContext
                    .Set<Product>()
                    .Include(p => p.Localizations)
                    .SingleOrDefault(c => c.Id == id);

                if (product == null)
                {
                    throw new HttpException(404, string.Format("product {0} doesn't exist", id.ToString()));
                }

                var model = new ProductOutput(product, this.LanguageCode);
                MainViewModel vm = CreateViewModel(model);

                //获取在同一个Caterage下的产品
                ViewBag.relatedProducts = GetRelatedPds(product.CategoryId);
                ViewBag.Title = "Product";
                return View(vm);
            }
        }

        /// <summary>
        /// 获取在同一个Caterage下的产品
        /// </summary>
        /// <param name="catId"></param>
        /// <returns></returns>
        private List<ProductOutput> GetRelatedPds(int catId)
        {
            List<ProductOutput> relatePds;
            using (var dbContext = new AllureContext())
            {
                relatePds = dbContext.Set<Product>()
                       .Include(p => p.Locale)
                       .Include(p => p.Localizations)
                       .Where(p => p.CategoryId == catId)
                       .ToArray()
                       .Select(p => new ProductOutput(p, this.LanguageCode))
                       .ToList();

            }

            if (relatePds == null || relatePds.Count == 0)
            {
                return null;
            }
            else
            {
                relatePds = GetRandomList(relatePds);
                return relatePds;
            }
        }
    }
}
