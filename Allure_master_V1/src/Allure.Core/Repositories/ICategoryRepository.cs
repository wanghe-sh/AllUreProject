using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Repositories
{
    public interface ICategoryRepository
    {
        void Add(Category category);

        void AddSub(SubCategory subCategory);

        void Update(Category category);

        void UpdateSub(SubCategory subCategory);

        Category Get(int id);

        SubCategory GetSub(int id);

        void Delete(Category category);

        void DeleteSub(SubCategory subCategory);

        IQueryable<Category> QueryAll();
    }
}
