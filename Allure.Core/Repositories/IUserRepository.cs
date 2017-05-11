using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria;
using Allure.Core.Criteria.Users;
using Allure.Core.Models;

namespace Allure.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> Get(int id);

        Task<SearchResult<User>> Search(SearchUser search);

        User Add(AddUser add);

        Task Update(int id, UpdateUser update);
    }
}
