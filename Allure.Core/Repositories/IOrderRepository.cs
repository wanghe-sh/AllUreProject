using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria;
using Allure.Core.Criteria.Orders;
using Allure.Core.Models;

namespace Allure.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> Get(int id);

        Task Update(int id, UpdateOrder update);

        Task<SearchResult<Order>> Search(SearchOrder search);
    }
}
