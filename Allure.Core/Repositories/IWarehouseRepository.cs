using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.Warehouses;
using Allure.Core.Models;

namespace Allure.Core.Repositories
{
    public interface IWarehouseRepository
    {
        Task<Warehouse> Get(int id);

        Task<IEnumerable<Warehouse>> GetAll();

        Warehouse Add(AddWarehouse add);

        Task Update(int id, UpdateWarehouse update);

        Task Delete(int id);
    }
}
