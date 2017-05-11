using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common.Unity;
using Allure.Core.Criteria.Categories;
using Allure.Core.Exceptions;
using Allure.Core.Models;
using Allure.Core.Repositories;
using Allure.Common.Validations;
using AutoMapper;

namespace Allure.Data.Repositories
{
    [Autowire(typeof(ICategoryRepository), Lifetime.PerResolve)]
    class CategoryRepository : ICategoryRepository
    {
        private readonly IDbContext _dbContext;

        public CategoryRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Category Add(AddCategory add)
        {
            var category = Mapper.Map<Category>(add);
            _dbContext.Set<Category>().Add(category);
            return category;
        }

        public async Task Delete(int id)
        {
            var category = await this.Get(id).ConfigureAwait(false);
            if (category == null)
            {
                throw new NotFoundException<Category>($"{nameof(Category.Id)}={id.ToString()}");
            }

            category.Deleted = true;
        }

        public async Task<Category> Get(int id)
        {
            var category = await _dbContext
                .Set<Category>()
                .SingleOrDefaultAsync(c => c.Id == id && !c.Deleted)
                .ConfigureAwait(false);

            category.Children = category.Children?.Where(c => !c.Deleted).ToList();
            return category;
        }

        public async Task<IEnumerable<Category>> GetRoots()
        {
            var categories = await _dbContext
                .Set<Category>()
                .Where(c => c.ParentId == null && !c.Deleted)
                .ToListAsync()
                .ConfigureAwait(false);

            foreach (var c in categories)
            {
                c.Children = c.Children?.Where(x => !x.Deleted).ToList();
            }

            return categories;
        }

        public async Task Update(int id, UpdateCategory update)
        {
            var category = await this.Get(id).ConfigureAwait(false);
            if (category == null)
            {
                throw new NotFoundException<Category>($"{nameof(Category.Id)}={id.ToString()}");
            }

            Mapper.Map(update, category);
        }
    }
}
