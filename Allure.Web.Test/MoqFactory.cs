using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core;
using Allure.Core.Models;
using Allure.Core.Repositories;
using Moq;

namespace Allure.Web.Test
{
    public class MoqFactory
    {
        public static IUnitOfWork CreateUnitOfWork(
            IBrandRepository brandRepository = null,
            ICategoryRepository categoryRepository = null,
            ILocaleRepository localeRepository = null)
        {
            var mock = new Mock<IUnitOfWork>();
            mock.SetupGet(m => m.Brands).Returns(brandRepository);
            mock.SetupGet(m => m.Categories).Returns(categoryRepository);
            mock.SetupGet(m => m.Locales).Returns(localeRepository);
            return mock.Object;            
        }
    }
}
