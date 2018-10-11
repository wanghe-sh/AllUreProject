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
    public class LogisticController : AdminControllerBase
    {
        [HttpGet]
        public Logistic[] List()
        {
            using (var dbContext = new AllureContext())
            {
                return dbContext.Set<Logistic>().ToArray();
            }
        }

        [HttpPost]
        public void Create(Logistic input)
        {
            using (var dbContext = new AllureContext())
            {
                dbContext.Set<Logistic>().Add(input);
                dbContext.SaveChanges();
            }
        }

        [HttpPost]
        public void Update(Logistic input)
        {
            using (var dbContext = new AllureContext())
            {                
                dbContext.UpdateGraph(input);
                dbContext.SaveChanges();
            }
        }

        [HttpPost]
        public void Delete(string id)
        {
            using (var dbContext = new AllureContext())
            {
                var logistic = dbContext.Set<Logistic>().SingleOrDefault(l => l.Code == id);

                if (logistic == null)
                {
                    throw new HttpException(404, string.Format("Logistic {0} doesn't exist", id.ToString()));
                }

                logistic.Status = DataStatus.Deleted;
                dbContext.SaveChanges();
            }
        }
    }
}
