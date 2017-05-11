using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.Locales;
using Allure.Core.Models;

namespace Allure.Core.Repositories
{
    public interface ILocaleRepository
    {
        Task<Locale> Get(int id);

        Task<IEnumerable<Locale>> GetAll();

        Locale Add(AddLocale add);

        Task Update(int id, UpdateLocale update);

        Task Delete(int id);
    }
}
