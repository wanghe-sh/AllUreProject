using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria;
using Allure.Core.Criteria.Products;
using Allure.Core.Models;

namespace Allure.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Product> Get(int id);

        Task<Product> Get(string number);

        Task<SearchResult<Product>> Search(SearchProduct search);

        Task<Product> Add(AddProduct add);

        Task Update(int id, UpdateProduct update);

        Task Delete(int id);
    }
}
