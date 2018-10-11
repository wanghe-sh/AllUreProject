using Allure.Admin.Models;
using Allure.Data;
using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.Entity;
using RefactorThis.GraphDiff;
using System.Net.Http;
using Newtonsoft.Json;
using Allure.Common;

namespace Allure.Admin.Controllers
{
    public class CategoryController : AdminControllerBase
    {
        [HttpGet]
        public CategoryOutput[] List()
        {
            using (var dbContext = new AllureContext())
            {
                var categories = dbContext
                    .Set<Category>()
                    .Include(c => c.Localized)
                    .Include(c => c.Children)
                    .ToList()
                    .Select(c => new CategoryOutput(c))
                    .ToArray();

                return categories;
            }
        }

        [HttpGet]
        public CategoryOutput Id(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var category = dbContext
                    .Set<Category>()
                    .Include(c => c.Localized)
                    .Include(c => c.Children)
                    .SingleOrDefault(c => c.Id == id);

                if (category == null)
                {
                    throw new HttpException(404, string.Format("category {0} deosn't exist", id.ToString()));
                }

                return new CategoryOutput(category);
            }
        }

        [HttpGet]
        public SubCategoryOutput SubId(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var subCategory = dbContext
                    .Set<SubCategory>()
                    .Include(c => c.Localized)
                    .SingleOrDefault(c => c.Id == id);

                if (subCategory == null)
                {
                    throw new HttpException(404, string.Format("subcategory {0} deosn't exist", id.ToString()));
                }

                return new SubCategoryOutput(subCategory);
            }
        }

        [HttpPost]
        public CategoryOutput Create(CategoryInput input)
        {
            var category = new Category
            {
                Localized = input.Localized.Select(i => new LocalizedCategory
                {
                    LanguageCode = i.LanguageCode,
                    Name = i.Name,
                    Description = i.Description
                }).ToList()
            };

            using (var dbContext = new AllureContext())
            {
                dbContext.Set<Category>().Add(category);
                dbContext.SaveChanges();
            }

            return Id(category.Id);
        }

        [HttpPost]
        public SubCategoryOutput CreateSub()
        {
            var httpRequest = HttpContext.Current.Request;

            var formData = httpRequest.Form["subcategory"];

            if (formData.IsNullOrEmpty())
            {
                throw new Exception("missing subcategory data");
            }

            if (httpRequest.Files.Count == 0)
            {
                throw new Exception("missing subcategory image");
            }

            var image = httpRequest.Files[0];
            var url = "/UploadImages/" + Guid.NewGuid().ToString() + new System.IO.FileInfo(image.FileName).Extension;
            image.SaveAs(HttpContext.Current.Server.MapPath("~" + url));

            var input = JsonConvert.DeserializeObject<SubCategoryInput>(formData);
            var subCategory = new SubCategory
            {
                ParentId = input.ParentId,
                ImageUrl = url,
                Localized = input.Localized
                   .Select(x => new LocalizedSubCategory
                   {
                       LanguageCode = x.LanguageCode,
                       Name = x.Name,
                       Description = x.Description
                   })
                   .ToList()
            };

            using (var dbContext = new AllureContext())
            {
                dbContext.Set<SubCategory>().Add(subCategory);
                dbContext.SaveChanges();
            }

            return SubId(subCategory.Id);
        }

        [HttpPost]
        public CategoryOutput Update(CategoryInput input)
        {
            var category = new Category
            {
                Id = input.Id,
                Localized = input.Localized
                    .Select(c => new LocalizedCategory
                    {
                        CategoryId = input.Id,
                        LanguageCode = c.LanguageCode,
                        Name = c.Name,
                        Description = c.Description
                    }).ToList()
            };

            using (var dbContext = new AllureContext())
            {
                dbContext.UpdateGraph(category, m => m.OwnedCollection(c => c.Localized));
                dbContext.SaveChanges();
            }

            return Id(category.Id);
        }
        
        [HttpPost]
        public SubCategoryOutput UpdateSub()
        {
            var httpRequest = HttpContext.Current.Request;

            var formData = httpRequest.Form["subcategory"];

            if (formData.IsNullOrEmpty())
            {
                throw new Exception("missing subcategory data");
            }

            var input = JsonConvert.DeserializeObject<SubCategoryInput>(formData);

            var subCategory = new SubCategory
            {
                Id = input.Id,
                ParentId = input.ParentId,
                ImageUrl = input.ImageUrl,
                Localized = input.Localized
                    .Select(c => new LocalizedSubCategory
                    {
                        SubCategoryId = input.Id,
                        LanguageCode = c.LanguageCode,
                        Name = c.Name,
                        Description = c.Description
                    }).ToList()
            };

            if (httpRequest.Files.Count > 0)
            {
                var image = httpRequest.Files[0];
                var url = "/UploadImages/" + Guid.NewGuid().ToString() + new System.IO.FileInfo(image.FileName).Extension;
                image.SaveAs(HttpContext.Current.Server.MapPath("~" + url));
                subCategory.ImageUrl = url;
            }

            using (var dbContext = new AllureContext())
            {
                dbContext.UpdateGraph(subCategory, m => m.OwnedCollection(c => c.Localized));
                dbContext.SaveChanges();
            }

            return SubId(subCategory.Id);
        }

        [HttpPost]
        public void Delete(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var category = dbContext
                    .Set<Category>()
                    .Include(c => c.Localized)
                    .Include(c => c.Children)
                    .SingleOrDefault(c => c.Id == id);

                if (category == null)
                {
                    throw new Exception(string.Format("category {0} doesn't exist", id.ToString()));
                }

                dbContext.Set<LocalizedCategory>().RemoveRange(category.Localized);
                dbContext.Set<LocalizedSubCategory>().RemoveRange(category.Children.SelectMany(c => c.Localized));
                dbContext.Set<SubCategory>().RemoveRange(category.Children);
                dbContext.Entry(category).State = EntityState.Deleted;
                dbContext.SaveChanges();
            }
        }

        [HttpPost]
        public void DeleteSub(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var subCategory = dbContext
                    .Set<SubCategory>()
                    .Include(c => c.Localized)                    
                    .SingleOrDefault(c => c.Id == id);

                if (subCategory == null)
                {
                    throw new Exception(string.Format("subcategory {0} doesn't exist", id.ToString()));
                }

                dbContext.Set<LocalizedSubCategory>().RemoveRange(subCategory.Localized);
                dbContext.Entry(subCategory).State = EntityState.Deleted;
                dbContext.SaveChanges();
            }
        }
    }
}