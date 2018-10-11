using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBrandRepository Brands { get; }

        ICategoryRepository Categories { get; }

        ILanguageRepository Languages { get; }

        ILocaleRepository Locales { get; }

        IProductRepository Products { get; }

        ISysTextRepository SysTexts { get; }

        IUserRepository Users { get; }
        
        void Commit();
    }
}
