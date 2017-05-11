using Allure.Data;
using Allure.Core.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading;
using Allure.Web.Models;
using Allure.Web.Utility;
using System.Collections.Generic;
using System;

namespace Allure.Web.Controllers
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
            BuildLanguagesInModel(model);

            BuildCategoryInModel(model);

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
                var categories = dbContext
                    .Set<Category>()
                    .Include(c => c.Localizations)
                    .Include(c => c.Children)
                    .Where(c => !c.Deleted && !c.ParentId.HasValue)
                    .ToList();

                foreach (var c in categories)
                {
                    c.Children = c.Children?.Where(x => !x.Deleted).ToList();
                }

                model.Categories = categories
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

        public static List<T> GetRandomList<T>(List<T> inputList)
        {
            //Copy to a array
            T[] copyArray = new T[inputList.Count];
            inputList.CopyTo(copyArray);

            //add range
            List<T> copuList = new List<T>();
            copuList.AddRange(copyArray);

            //Set outputList and random
            List<T> outputList = new List<T>();
            Random rd = new Random(DateTime.Now.Millisecond);

            while (outputList.Count <= 4 && copuList.Count > 0)
            {
                // Select an index and item
                int rdIndex = rd.Next(0, copuList.Count - 1);
                T remove = copuList[rdIndex];

                //remove it from copyList and add it to output
                copuList.Remove(remove);
                outputList.Add(remove);
            }

            return outputList;
        }
    }
}
