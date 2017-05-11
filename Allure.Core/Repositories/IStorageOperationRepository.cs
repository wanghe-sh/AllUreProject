using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria;
using Allure.Core.Criteria.StorageOperations;
using Allure.Core.Models;

namespace Allure.Core.Repositories
{
    public interface IStorageOperationRepository
    {
        Task<StorageOperation> Get(int id);

        Task<SearchResult<StorageOperation>> Search(SearchStorageOperation search);

        Task<StorageOperation> Add(AddStorageOperation add, int operatorId);

        Task Update(int id, UpdateStorageOperation update, int operatorId);
    }
}
