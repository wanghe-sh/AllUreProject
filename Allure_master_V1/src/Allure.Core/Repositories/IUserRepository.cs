using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Repositories
{
    public interface IUserRepository
    {
        User Get(int id);

        void Add(User user);

        void Delete(User user);

        void Update(User user);

        IQueryable<User> QueryAll();
    }
}
