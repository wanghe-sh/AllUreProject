using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Repositories
{
    public interface ILocaleRepository
    {
        Locale Get(int id);

        void Add(Locale locale);

        void Update(Locale locale);

        void Delete(Locale locale);

        IQueryable<Locale> Query();
    }
}
