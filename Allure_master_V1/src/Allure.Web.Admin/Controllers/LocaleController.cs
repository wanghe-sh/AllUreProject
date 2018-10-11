using Allure.Data;
using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using RefactorThis.GraphDiff;
using System.Data.Entity;
using Allure.Common;
using Allure.Admin.Models;

namespace Allure.Admin.Controllers
{
    public class LocaleController : AdminControllerBase
    {
        [HttpGet]
        public LocaleOutput[] List()
        {
            using (var dbContext = new AllureContext())
            {
                return dbContext
                    .Set<Locale>()
                    .Include(l => l.Localized)
                    .ToArray()
                    .Select(l => new LocaleOutput(l))
                    .ToArray();
            }
        }

        [HttpGet]
        public LocaleOutput Id(int id, string lang)
        {
            using (var dbContext = new AllureContext())
            {
                var locale = dbContext
                    .Set<Locale>()
                    .Include(l => l.Localized)
                    .SingleOrDefault(l => l.Id == id);

                if (locale == null)
                {
                    throw new HttpException(404, string.Format("locale {0} doesn't exist", id.ToString()));
                }

                return new LocaleOutput(locale);
            }
        }

        [HttpPost]
        public LocaleOutput Create(Locale locale)
        {
            using (var dbContext = new AllureContext())
            {
                dbContext.Set<Locale>().Add(locale);
                dbContext.SaveChanges();
            }

            return new LocaleOutput(locale);
        }

        [HttpPost]
        public LocaleOutput Update(Locale locale)
        {
            using (var dbContext = new AllureContext())
            {
                dbContext.UpdateGraph(locale, m => m.OwnedCollection(l => l.Localized));
                dbContext.SaveChanges();
            }

            return new LocaleOutput(locale);
        }

        [HttpPost]
        public void Delete(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var locale = dbContext
                    .Set<Locale>()
                    .Include(l => l.Localized)
                    .SingleOrDefault(l => l.Id == id);

                if (locale == null)
                {
                    throw new Exception(string.Format("locale {0} doesn't exist", id.ToString()));
                }

                locale.Status = DataStatus.Deleted;
                dbContext.SaveChanges();
            }
        }
    }
}