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
    public class WarehouseController : AdminControllerBase
    {
        [HttpGet]
        public Warehouse[] List()
        {
            using (var dbContext = new AllureContext())
            {
                return dbContext.Set<Warehouse>().ToArray();
            }
        }

        [HttpPost]
        public void Create(Warehouse input)
        {
            using (var dbContext = new AllureContext())
            {
                dbContext.Set<Warehouse>().Add(input);
                dbContext.SaveChanges();
            }
        }

        [HttpPost]
        public void Update(Warehouse input)
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
                var warehouse = dbContext.Set<Warehouse>().SingleOrDefault(l => l.Code == id);

                if (warehouse == null)
                {
                    throw new HttpException(404, string.Format("Warehouse {0} doesn't exist", id.ToString()));
                }

                warehouse.Status = DataStatus.Deleted;
                dbContext.SaveChanges();
            }
        }
    }
}
