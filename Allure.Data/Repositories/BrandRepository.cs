using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common.Unity;
using Allure.Core.Criteria.Brands;
using Allure.Core.Exceptions;
using Allure.Core.Models;
using Allure.Core.Repositories;
using Allure.Common.Validations;
using AutoMapper;

namespace Allure.Data.Repositories
{
    [Autowire(typeof(IBrandRepository), Lifetime.PerResolve)]
    class BrandRepository : IBrandRepository
    {
        private readonly IDbContext _dbContext;

        public BrandRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Brand Add(AddBrand add)
        {
            var brand = Mapper.Map<Brand>(add);
            _dbContext.Set<Brand>().Add(brand);
            return brand;
        }

        public async Task Delete(int id)
        {
            var brand = await this.Get(id).ConfigureAwait(false);
            if (brand == null)
            {
                throw new NotFoundException<Brand>($"{nameof(Brand.Id)}={id.ToString()}");
            }

            brand.Deleted = true;
        }

        public async Task<Brand> Get(int id)
        {
            return await _dbContext
                .Set<Brand>()
                .SingleOrDefaultAsync(b => b.Id == id && !b.Deleted)
                .ConfigureAwait(false);
        }

        public async Task<Brand> Get(string name)
        {
            return await _dbContext
                .Set<Brand>()
                .SingleOrDefaultAsync(b => b.Name == name)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Brand>> GetAll()
        {
            return await _dbContext
                .Set<Brand>()
                .Where(b => !b.Deleted)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task Update(int id, UpdateBrand update)
        {
            var brand = await this.Get(id).ConfigureAwait(false);
            if (brand == null)
            {
                throw new NotFoundException<Brand>($"{nameof(Brand.Id)}={id.ToString()}");
            }

            Mapper.Map(update, brand);            
        }
    }
}
