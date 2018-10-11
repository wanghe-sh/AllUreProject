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
    public class ProductController : AdminControllerBase
    {
        [HttpPost]
        public SearchProductOutput Search(SearchProductInput input)
        {
            using (var dbContext = new AllureContext())
            {
                IQueryable<Product> query = dbContext
                    .Set<Product>()
                    .Include(p => p.Localized)
                    .Include(p => p.Images);

                if (input.BrandId.HasValue)
                {
                    query = query.Where(p => p.BrandId == input.BrandId.Value);
                }

                if (!input.Number.IsNullOrEmpty())
                {
                    query = query.Where(p => p.Number == input.Number);
                }

                if (!input.Locale.IsNullOrEmpty())
                {
                    query = query.Where(p => p.Locale.Localized.Any(l => l.Name.Contains(input.Locale)));
                }

                if (!input.Name.IsNullOrEmpty())
                {
                    query = query.Where(p => p.Name.Contains(input.Name));
                }

                if (input.MaxPrice.HasValue)
                {
                    query = query.Where(p => p.Price <= input.MaxPrice.Value);
                }

                if (input.MinPrice.HasValue)
                {
                    query = query.Where(p => p.Price >= input.MinPrice.Value);
                }

                var orderBy = "DisplayOrder";
                if (!input.OrderBy.IsNullOrEmpty())
                {
                    orderBy = input.OrderBy + "," + orderBy;
                }

                var result = new SearchProductOutput();
                result.Count = query.Count();

                var pageSize = input.PageSize.GetValueOrDefault(10);
                var pageNumber = input.PageNumber.GetValueOrDefault(1) - 1;

                result.Products = query
                    .OrderBy(orderBy)
                    .Skip(pageNumber * pageSize)
                    .Take(pageSize)
                    .Where(p => p.Status == DataStatus.Normal)
                    .ToArray()
                    .Select(p => new ProductOutput(p))
                    .ToArray();

                return result;
            }
        }

        [HttpGet]
        public ProductOutput Id(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var product = dbContext
                    .Set<Product>()
                    .Include(p => p.Localized)
                    .Include(p => p.Images)
                    .SingleOrDefault(p => p.Id == id);

                if (product == null)
                {
                    throw new HttpException(404, string.Format("product {0} doesn't exist", id.ToString()));
                }

                return new ProductOutput(product);
            }
        }

        [HttpPost]
        public ProductOutput Create()
        {
            var httpRequest = HttpContext.Current.Request;

            var formData = httpRequest.Form["product"];

            if (formData.IsNullOrEmpty())
            {
                throw new Exception("missing product data");
            }

            if (httpRequest.Files.Count == 0)
            {
                throw new Exception("missing product image/video");
            }

            var input = JsonConvert.DeserializeObject<ProductInput>(formData);
            var product = new Product
            {
                BrandId = input.BrandId,
                SubCategoryId = input.SubCategoryId,
                LocaleId = input.LocaleId,
                VideoUrl = input.VideoUrl,
                Localized = input.Localized.Select(l => new LocalizedProduct { LanguageCode = l.LanguageCode, Description = l.Descrption }).ToList(),
                Start = input.Start,
                End = input.End,
                Number = input.Number,
                Price = input.Price,
                DisplayOrder = input.DisplayOrder,
                Name = input.Name,
                RecommendedL = input.RecommendedL,
                RecommendedS = input.RecommendedS,
                CreateDate = DateTime.Today,
                Storage = new ProductStorage { Balance = 0m, Frozen = 0m }
            };

            for (int i = 0; i < httpRequest.Files.Count; i++)
            {
                var file = httpRequest.Files[i];
                var extension = new System.IO.FileInfo(file.FileName).Extension;
                var imageUrl = "/UploadImages/" + Guid.NewGuid().ToString() + extension;
                var image = Image.FromStream(file.InputStream);
                image.Save(HttpContext.Current.Server.MapPath("~" + imageUrl));

                var thumbnail = image.GetThumbnailImage(150, (int)Math.Floor(image.Height * 150d / image.Width), () => false, IntPtr.Zero);
                var thumbnailurl = "/UploadImages/" + Guid.NewGuid().ToString() + extension;
                thumbnail.Save(HttpContext.Current.Server.MapPath("~" + thumbnailurl));

                product.Images.Add(new ProductImage { ImageUrl = imageUrl, ThumbnailUrl = thumbnailurl, Height = image.Height, Width = image.Width });
            }

            if (product.Images.All(i => i.IsLargeImage) && input.RecommendedS)
            {
                throw new Exception("a small image is expected");
            }

            if (product.Images.All(i => !i.IsLargeImage) && input.RecommendedL)
            {
                throw new Exception("a large image is expected");
            }

            using (var dbContext = new AllureContext())
            {
                dbContext.Set<Product>().Add(product);
                dbContext.SaveChanges();
            }

            return Id(product.Id);
        }

        [HttpPost]
        public ProductOutput Update()
        {
            var httpRequest = HttpContext.Current.Request;

            var formData = httpRequest.Form["product"];

            if (formData.IsNullOrEmpty())
            {
                throw new Exception("missing product data");
            }

            var input = JsonConvert.DeserializeObject<ProductInput>(formData);
            var product = new Product
            {
                Id = input.Id,
                BrandId = input.BrandId,
                SubCategoryId = input.SubCategoryId,
                LocaleId = input.LocaleId,
                VideoUrl = input.VideoUrl,
                Images = input.Images.Select(i => new ProductImage { Id = i.Id, ProductId = input.Id, ImageUrl = i.ImageUrl, ThumbnailUrl = i.ThumbnailUrl, Height = i.Height, Width = i.Width }).ToList(),
                Localized = input.Localized.Select(l => new LocalizedProduct { LanguageCode = l.LanguageCode, Description = l.Descrption }).ToList(),
                Start = input.Start,
                End = input.End,
                Number = input.Number,
                Price = input.Price,
                DisplayOrder = input.DisplayOrder,
                Name = input.Name,
                RecommendedL = input.RecommendedL,
                RecommendedS = input.RecommendedS
            };

            for (int i = 0; i < httpRequest.Files.Count; i++)
            {
                var file = httpRequest.Files[i];
                var extension = new System.IO.FileInfo(file.FileName).Extension;
                var imageUrl = "/UploadImages/" + Guid.NewGuid().ToString() + extension;
                var image = Image.FromStream(file.InputStream);
                image.Save(HttpContext.Current.Server.MapPath("~" + imageUrl));

                var thumbnail = image.GetThumbnailImage(150, (int)Math.Floor(image.Height * 150d / image.Width), () => false, IntPtr.Zero);
                var thumbnailurl = "/UploadImages/" + Guid.NewGuid().ToString() + extension;
                thumbnail.Save(HttpContext.Current.Server.MapPath("~" + thumbnailurl));

                product.Images.Add(new ProductImage { ImageUrl = imageUrl, ThumbnailUrl = thumbnailurl, Height = image.Height, Width = image.Width });
            }

            if (product.Images.All(i => i.IsLargeImage) && input.RecommendedS)
            {
                throw new Exception("a small image is expected");
            }

            if (product.Images.All(i => !i.IsLargeImage) && input.RecommendedL)
            {
                throw new Exception("a large image is expected");
            }

            using (var dbContext = new AllureContext())
            {
                var origin = dbContext.Set<Product>().AsNoTracking().SingleOrDefault(p => p.Id == product.Id);

                if (origin == null)
                {
                    throw new HttpException(404, string.Format("product {0} doesn't exist.", product.Id.ToString()));
                }

                product.CreateDate = origin.CreateDate;

                dbContext.UpdateGraph(product, p => p
                    .OwnedCollection(pp => pp.Localized)
                    .OwnedCollection(pp => pp.Images));


                dbContext.SaveChanges();
            }

            return Id(product.Id);
        }

        [HttpPost]
        public void Delete(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var product = dbContext
                    .Set<Product>()
                    .Include(p => p.Localized)
                    .Include(p => p.Images)
                    .Include(p => p.Storage)
                    .SingleOrDefault(p => p.Id == id);

                if (product == null)
                {
                    throw new HttpException(404, string.Format("product {0} doesn't exist", id.ToString()));
                }

                product.Status = DataStatus.Deleted;
                dbContext.SaveChanges();
            }
        }
    }
}