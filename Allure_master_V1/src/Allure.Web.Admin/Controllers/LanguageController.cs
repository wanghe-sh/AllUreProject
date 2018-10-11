using Allure.Admin.Models;
using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using Allure.Data;
using RefactorThis.GraphDiff;
using System.Data.Entity;

namespace Allure.Admin.Controllers
{
    public class LanguageController : AdminControllerBase
    {
        [HttpGet]
        public LanguageOutput[] Current()
        {
            using (var dbContext = new AllureContext())
            {
                var languages = dbContext
                    .Set<Language>()
                    .Where(l => l.Enabled)
                    .ToList()
                    .Select(lang => new LanguageOutput(lang))
                    .ToArray();

                return languages;
            }
        }

        [HttpGet]
        public LanguageOutput[] Potential()
        {
            return CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Select(c => new LanguageOutput(new Language { Code = c.Name }))
                .ToArray();
        }

        [HttpPost]
        public void Add(string id)
        {
            using (var dbContext = new AllureContext())
            {
                var supported = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

                if (supported.Any(c => c.Name.Equals(id)))
                {
                    var lang = dbContext.Set<Language>().SingleOrDefault(l => l.Code == id);

                    if (lang == null)
                    {
                        lang = new Language { Code = id };
                    }

                    lang.Enabled = true;

                    dbContext.SaveChanges();
                }
            }
        }

        [HttpPost]
        public void Delete(string id)
        {
            using (var dbContext = new AllureContext())
            {
                var lang = dbContext.Set<Language>().SingleOrDefault(l => l.Code == id);

                if (lang != null)
                {
                    lang.Enabled = false;
                    dbContext.SaveChanges();
                }
            }
        }

        [HttpPost]
        public void SetDefault(string id)
        {
            using (var dbContext = new AllureContext())
            {
                var defaultLang = dbContext.Set<Language>().Single(l => l.IsDefault);

                if (!defaultLang.Code.Equals(id))
                {
                    var lang = dbContext.Set<Language>().SingleOrDefault(l => l.Code == id);
                    if (lang == null)
                    {
                        throw new Exception(string.Format("the language {0} isn't active yet.", id));
                    }

                    lang.IsDefault = true;
                    defaultLang.IsDefault = false;
                    dbContext.SaveChanges();
                }
            }
        }
    }
}