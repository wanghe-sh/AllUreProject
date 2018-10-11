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
    public class HomePageImageController : AdminControllerBase
    {
        [HttpGet]
        public HomePageImageOutput Id(string id)
        {
            using (var dbContext = new AllureContext())
            {
                var image = dbContext
                    .Set<HomePageImage>()
                    .Include(i => i.Localized)
                    .SingleOrDefault(i => i.Id == id);

                if (image == null)
                {
                    throw new HttpException(404, string.Format("image {0} doesn't exist", id.ToString()));
                }

                return new HomePageImageOutput(image);
            }
        }

        [HttpPost]
        public HomePageImageOutput Create()
        {
            var httpRequest = HttpContext.Current.Request;

            var formData = httpRequest.Form["image"];

            if (formData.IsNullOrEmpty())
            {
                throw new Exception("missing image data");
            }

            if (httpRequest.Files.Count == 0)
            {
                throw new Exception("missing image file");
            }

            var input = JsonConvert.DeserializeObject<HomePageImageInput>(formData);
            var image = new HomePageImage
            {
                Id = input.Id,
                Width = input.Width,
                Height = input.Height,
                NavigateUrl = input.NaviateUrl,
                Localized = input.Localized.Select(l => new LocalizedHomePageImage
                    {
                        HomePageImageId = input.Id,
                        LanguageCode = l.LanguageCode,
                        Title = l.Title,
                        Description = l.Descrption
                    }).ToList()
            };

            var file = httpRequest.Files[0];
            var extension = new System.IO.FileInfo(file.FileName).Extension;
            var imageUrl = "/UploadImages/" + Guid.NewGuid().ToString() + extension;
            var imageFile = Image.FromStream(file.InputStream);
            imageFile.Save(HttpContext.Current.Server.MapPath("~" + imageUrl));
            image.ImageUrl = imageUrl;

            using (var dbContext = new AllureContext())
            {
                dbContext.Set<HomePageImage>().Add(image);
                dbContext.SaveChanges();
            }

            return Id(image.Id);
        }

        [HttpPost]
        public HomePageImageOutput Update()
        {
            var httpRequest = HttpContext.Current.Request;

            var formData = httpRequest.Form["image"];

            if (formData.IsNullOrEmpty())
            {
                throw new Exception("missing image data");
            }

            var input = JsonConvert.DeserializeObject<HomePageImageInput>(formData);
            var image = new HomePageImage
            {
                Id = input.Id,
                Width = input.Width,
                Height = input.Height,
                NavigateUrl = input.NaviateUrl,
                Localized = input.Localized.Select(l => new LocalizedHomePageImage
                {
                    HomePageImageId = input.Id,
                    LanguageCode = l.LanguageCode,
                    Title = l.Title,
                    Description = l.Descrption
                }).ToList()
            };

            if (httpRequest.Files.Count > 0)
            {
                var file = httpRequest.Files[0];
                var extension = new System.IO.FileInfo(file.FileName).Extension;
                var imageUrl = "/UploadImages/" + Guid.NewGuid().ToString() + extension;
                var imageFile = Image.FromStream(file.InputStream);
                imageFile.Save(HttpContext.Current.Server.MapPath("~" + imageUrl));
                image.ImageUrl = imageUrl;
            }

            using (var dbContext = new AllureContext())
            {
                dbContext.UpdateGraph(image, i => i.OwnedCollection(ii => ii.Localized));
                dbContext.SaveChanges();
            }

            return Id(image.Id);
        }

        [HttpPost]
        public void Delete(string id)
        {
            using (var dbContext = new AllureContext())
            {
                var image = dbContext
                    .Set<HomePageImage>()
                    .Include(i => i.Localized)
                    .SingleOrDefault(i => i.Id == id);

                if (image == null)
                {
                    throw new HttpException(404, string.Format("image {0} doesn't exist", id.ToString()));
                }

                dbContext.Set<LocalizedHomePageImage>().RemoveRange(image.Localized);
                dbContext.Entry(image).State = EntityState.Deleted;
                dbContext.SaveChanges();
            }
        }
    }
}