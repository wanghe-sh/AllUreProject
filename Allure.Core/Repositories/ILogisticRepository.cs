using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.Logistics;
using Allure.Core.Models;

namespace Allure.Core.Repositories
{
    public interface ILogisticRepository
    {
        Task<Logistic> Get(int id);

        Task<IEnumerable<Logistic>> GetAll();

        Logistic Add(AddLogistic add);

        Task Update(int id, UpdateLogistic update);

        Task Delete(int id);
    }
}
