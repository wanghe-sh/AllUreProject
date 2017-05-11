using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.Categories;
using Allure.Core.Models;

namespace Allure.Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> Get(int id);

        Task<IEnumerable<Category>> GetRoots();

        Category Add(AddCategory add);

        Task Update(int id, UpdateCategory update);

        Task Delete(int id);
    }
}
