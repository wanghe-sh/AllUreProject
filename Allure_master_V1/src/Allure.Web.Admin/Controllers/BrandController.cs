using Allure.Data;
using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using RefactorThis.GraphDiff;
using System.Data.Entity;

namespace Allure.Admin.Controllers
{
    public class BrandController : AdminControllerBase
    {
        [HttpGet]
        public Brand[] List()
        {
            using (var dbContext = new AllureContext())
            {
                return dbContext.Set<Brand>().ToArray();
            }
        }

        [HttpGet]
        public Brand Id(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var brand = dbContext.Set<Brand>().SingleOrDefault(b => b.Id == id);

                if (brand == null)
                {
                    throw new HttpException(404, string.Format("brand {0} doesn't exist", id.ToString()));
                }

                return brand;
            }
        }

        [HttpPost]
        public Brand Create(Brand brand)
        {
            using (var dbContext = new AllureContext())
            {
                dbContext.Set<Brand>().Add(brand);
                dbContext.SaveChanges();
            }

            return Id(brand.Id);
        }

        [HttpPost]
        public Brand Update(Brand brand)
        {
            using (var dbContext = new AllureContext())
            {
                dbContext.UpdateGraph(brand);
                dbContext.SaveChanges();
            }

            return Id(brand.Id);
        }

        [HttpPost]
        public void Delete(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var brand = dbContext.Set<Brand>().SingleOrDefault(b => b.Id == id);

                if (brand == null)
                {
                    throw new Exception(string.Format("brand {0} doesn't exist", id.ToString()));
                }

                brand.Status = DataStatus.Deleted;
                dbContext.SaveChanges();
            }
        }
    }
}