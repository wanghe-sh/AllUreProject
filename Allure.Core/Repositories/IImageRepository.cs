using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.Images;
using Allure.Core.Models;

namespace Allure.Core.Repositories
{
    public interface IImageRepository
    {
        Image Add(AddImage add);
    }
}
