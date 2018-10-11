using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> QueryAll();

        void Add(Product product);
    }
}
