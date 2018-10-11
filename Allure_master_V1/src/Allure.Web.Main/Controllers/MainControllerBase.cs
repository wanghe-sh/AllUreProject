using Allure.Data;
using Allure.Core.Models;
using Allure.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading;
using Allure.Web;
using System.Configuration;

namespace Allure.UI.Controllers
{
    public abstract class MainControllerBase : Controller
    {
        public AllureIdentity Identity
        {
            get { return this.HttpContext.User.Identity as AllureIdentity; }
        }

        public string LanguageCode 
        { 
            get 
            {
                return Thread.CurrentThread.CurrentUICulture.Name; 
            } 
        }

        public MainViewModel CreateViewModel()
        {
            var model = new MainViewModel();

            BuildUserInModel(model);
            BuildCategoryInModel(model);
            BuildLanguagesInModel(model);

            return model;
        }

        public MainViewModel<T> CreateViewModel<T>(T bodyModel)
        {
            var model = new MainViewModel<T>(bodyModel);

            BuildUserInModel(model);
            BuildCategoryInModel(model);
            BuildLanguagesInModel(model);

            return model;
        }

        private void BuildUserInModel(MainViewModel model)
        {
            model.User = this.Identity.User;
        }

        private void BuildCategoryInModel(MainViewModel model)
        {
            using (var dbContext = new AllureContext())
            {
                model.Categories = dbContext
                    .Set<Category>()
                    .Include(c => c.Localized)
                    .Include(c => c.Children)
                    .ToList()
                    .Select(c => new CategoryOutput(c, this.LanguageCode))
                    .ToArray();
            }
        }

        private void BuildLanguagesInModel(MainViewModel model)
        { 
            using (var dbContext = new AllureContext())
            {
                model.Languages = dbContext
                    .Set<Language>()
                    .Where(lang => lang.Enabled)
                    .ToArray();
            }
        }
    }
}
