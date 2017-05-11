using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common.Unity;
using Allure.Core.Criteria.Warehouses;
using Allure.Core.Exceptions;
using Allure.Core.Models;
using Allure.Core.Repositories;
using Allure.Common.Validations;
using AutoMapper;

namespace Allure.Data.Repositories
{
    [Autowire(typeof(IWarehouseRepository), Lifetime.PerResolve)]
    class WarehouseRepository : IWarehouseRepository
    {
        private readonly IDbContext _dbContext;

        public WarehouseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Warehouse Add(AddWarehouse add)
        {
            var warehouse = Mapper.Map<Warehouse>(add);
            _dbContext.Set<Warehouse>().Add(warehouse);
            return warehouse;
        }

        public async Task Delete(int id)
        {
            var warehouse = await this.Get(id).ConfigureAwait(false);
            if (warehouse == null)
            {                
                throw new NotFoundException<Warehouse>($"{nameof(Warehouse.Id)}={id.ToString()}");
            }

            warehouse.Deleted = true;
        }

        public async Task<Warehouse> Get(int id)
        {
            return await _dbContext
                .Set<Warehouse>()
                .SingleOrDefaultAsync(w => w.Id == id && !w.Deleted)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Warehouse>> GetAll()
        {
            return await _dbContext
                .Set<Warehouse>()
                .Where(l => !l.Deleted)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task Update(int id, UpdateWarehouse update)
        {
            var warehouse = await this.Get(id).ConfigureAwait(false);
            if (warehouse == null)
            {
                throw new NotFoundException<Warehouse>($"{nameof(Warehouse.Id)}={id.ToString()}");
            }

            Mapper.Map(update, warehouse);
        }
    }
}
