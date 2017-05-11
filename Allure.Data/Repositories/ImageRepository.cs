using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common.Unity;
using Allure.Core.Criteria.Images;
using Allure.Core.Models;
using Allure.Core.Repositories;
using AutoMapper;

namespace Allure.Data.Repositories
{
    [Autowire(typeof(IImageRepository), Lifetime.PerResolve)]
    class ImageRepository : IImageRepository
    {
        private readonly IDbContext _dbContext;

        public ImageRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Image Add(AddImage add)
        {
            var image = Mapper.Map<Image>(add);
            _dbContext.Set<Image>().Add(image);
            return image;
        }
    }
}
