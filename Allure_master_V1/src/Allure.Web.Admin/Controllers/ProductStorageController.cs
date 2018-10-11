using Allure.Admin.Models;
using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Allure.Common;
using System.Linq.Expressions;
using Allure.Data;
using RefactorThis.GraphDiff;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;

namespace Allure.Admin.Controllers
{
    public class ProductStorageController : AdminControllerBase
    {
        [HttpPost]
        public SearchProductStorageOutput Search(SearchProductStorageInput input)
        {
            using (var dbContext = new AllureContext())
            {
                IQueryable<Product> query = dbContext
                    .Set<Product>()
                    .Include(p => p.Storage)
                    .Include(p => p.Localized);

                if (!input.Number.IsNullOrEmpty())
                {
                    query = query.Where(p => p.Number == input.Number);
                }

                if (!input.Name.IsNullOrEmpty())
                {
                    query = query.Where(p => p.Name.Contains(input.Name));
                }

                if (input.BrandId.HasValue)
                {
                    query = query.Where(p => p.BrandId == input.BrandId.Value);
                }

                if (input.SubCategoryId.HasValue)
                {
                    query = query.Where(p => p.SubCategoryId == input.SubCategoryId.Value);
                }
                else
                {
                    if (input.CategoryId.HasValue)
                    {
                        var subCategoryIds = dbContext
                            .Set<SubCategory>()
                            .Where(c => c.ParentId == input.CategoryId.Value)
                            .Select(c => c.Id)
                            .ToList();

                        query = query.Where(p => subCategoryIds.Contains(p.SubCategoryId));
                    }
                }

                if (input.CreateDate.HasValue)
                {
                    query = query.Where(p => p.CreateDate == input.CreateDate.Value);
                }

                if (input.Start.HasValue)
                {
                    query = query.Where(p => p.Start == input.Start.Value);
                }

                if (input.End.HasValue)
                {
                    query = query.Where(p => p.End == input.End.Value);
                }

                var result = new SearchProductStorageOutput();
                result.Count = query.Count();

                var pageSize = input.PageSize.GetValueOrDefault(10);
                var pageNumber = input.PageNumber.GetValueOrDefault(1) - 1;

                result.Storages = query
                    .OrderBy(p => p.Id)
                    .Skip(pageNumber * pageSize)
                    .Take(pageSize)
                    .ToArray()
                    .Select(p => new ProductStorageOutput(p))
                    .ToArray();

                return result;
            }
        }
    }
}