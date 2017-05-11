using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common.Unity;
using Allure.Core;
using Allure.Core.Repositories;

namespace Allure.Data
{
    [Autowire(typeof(IUnitOfWork), Lifetime.PerResolve)]
    class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;

        public UnitOfWork(
            IDbContext dbContext,
            ILanguageRepository languageRepository,
            IBrandRepository brandRepository,
            ICheckAddressRepository checkAddressRepository,
            ICategoryRepository categoryRepository,
            ILocaleRepository localeRepository,
            ILogisticRepository logisticRepository,
            IWarehouseRepository warehouseRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IStorageOperationRepository storageOperationRepository,
            IUserRepository userRepository,
            IImageRepository imageRepository)
        {
            _dbContext = dbContext;

            this.Languages = languageRepository;
            this.Brands = brandRepository;
            this.CheckAddresses = checkAddressRepository;
            this.Categories = categoryRepository;
            this.Locales = localeRepository;
            this.Logistics = logisticRepository;
            this.Warehouses = warehouseRepository;
            this.Products = productRepository;
            this.Orders = orderRepository;
            this.StorageOperations = storageOperationRepository;
            this.Users = userRepository;
            this.Images = imageRepository;
        }

        public ILanguageRepository Languages { get; private set; }

        public IBrandRepository Brands { get; private set; }

        public ICheckAddressRepository CheckAddresses { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public ILocaleRepository Locales { get; private set; }

        public ILogisticRepository Logistics { get; private set; }

        public IWarehouseRepository Warehouses { get; private set; }

        public IProductRepository Products { get; private set; }

        public IOrderRepository Orders { get; private set; }

        public IStorageOperationRepository StorageOperations { get; private set; }

        public IUserRepository Users { get; private set; }

        public IImageRepository Images { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        #region IDisposable

        private bool _disposed = false;

        void IDisposable.Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        #endregion
    }
}
