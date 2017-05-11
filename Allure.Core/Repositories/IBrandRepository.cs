using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.Brands;
using Allure.Core.Models;

namespace Allure.Core.Repositories
{
    public interface IBrandRepository
    {
        Task<Brand> Get(int id);

        Task<Brand> Get(string name);

        Task<IEnumerable<Brand>> GetAll();

        Brand Add(AddBrand add);

        Task Update(int id, UpdateBrand brand);

        Task Delete(int id);
    }
}