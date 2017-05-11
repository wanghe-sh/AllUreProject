using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Repositories;

namespace Allure.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ILanguageRepository Languages { get; }

        IBrandRepository Brands { get; }

        ICheckAddressRepository CheckAddresses { get; }

        ICategoryRepository Categories { get; }

        ILocaleRepository Locales { get; }

        ILogisticRepository Logistics { get; }

        IWarehouseRepository Warehouses { get; }

        IProductRepository Products { get; }

        IOrderRepository Orders { get; }

        IStorageOperationRepository StorageOperations { get; }

        IUserRepository Users { get; }

        IImageRepository Images { get; }

        Task<int> SaveChangesAsync();
    }
}
