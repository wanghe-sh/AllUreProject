using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.Brands;
using Allure.Core.Models;

namespace Allure.Core.Repositories
{
    public interface ICheckAddressRepository
    {
        Task<CheckAddress> Get(int id);
        
        Task Update(int id, Criteria.CheckAddresses.UpdateCheckAddress checkAddress);

    }
}