using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Allure.Common;
using Allure.Core;
using Allure.Core.Criteria.Images;
using Allure.Core.Models;
using Allure.Web.Areas.Admin.Models.Images;
using AutoMapper;

namespace Allure.Web.Areas.Admin.Controllers
{
    public class ImagesController : ApiControllerBase
    {
        private static readonly string ImageFolderName = "upload";
        private static readonly string[] SupportedImageExtensions = new[] { ".jpg", ".jpeg", ".gif", ".bmp", ".png", ".tif" };
        private static readonly int ThumbnailWidth = 150;

        public ImagesController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        /// <summary>
        /// upload an image
        /// </summary>
        /// <returns>the image information</returns>
        [ResponseType(typeof(ViewImage))]
        public async Task<IHttpActionResult> Post()
        {
            var request = HttpContext.Current.Request;
            if (request.Files.Count == 0)
            {
                return BadRequest();
            }

            var file = request.Files[0];
            var ext = new System.IO.FileInfo(file.FileName).Extension;
            if (!ext.In(StringComparer.OrdinalIgnoreCase, SupportedImageExtensions))
            {
                return BadRequest($"the image type {ext} is not supported");
            }
            
            var img = System.Drawing.Image.FromStream(file.InputStream);
            var url = GenerateUrl(ext);
            img.Save(HttpContext.Current.Server.MapPath("~" + url));
            
            var thumbnail = img.GetThumbnailImage(ThumbnailWidth, (int)Math.Floor(img.Height * ThumbnailWidth * 1d / img.Width), () => false, IntPtr.Zero);
            var thumbnailUrl = GenerateUrl(ext);
            thumbnail.Save(HttpContext.Current.Server.MapPath("~" + thumbnailUrl));

            var add = new AddImage
            {
                Width = img.Width,
                Height = img.Height,
                Url = url,
                ThumbnailUrl = thumbnailUrl
            };

            var image = _unitOfWork.Images.Add(add);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var view = Mapper.Map<ViewImage>(image);
            return Ok(view);
        }

        private string GenerateUrl(string ext)
        {
            return $"/{ImageFolderName}/{Guid.NewGuid().ToString()}{ext}";
        }
    }
}