using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common;
using Allure.Common.Unity;
using Allure.Core.Criteria;
using Allure.Core.Criteria.StorageOperations;
using Allure.Core.Exceptions;
using Allure.Core.Models;
using Allure.Core.Repositories;
using Allure.Common.Validations;
using AutoMapper;

namespace Allure.Data.Repositories
{
    [Autowire(typeof(IStorageOperationRepository), Lifetime.PerResolve)]
    class StorageOperationRepository : IStorageOperationRepository
    {
        private readonly IDbContext _dbContext;

        public StorageOperationRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StorageOperation> Get(int id)
        {
            return await _dbContext
                .Set<StorageOperation>()
                .SingleOrDefaultAsync(o => o.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<SearchResult<StorageOperation>> Search(SearchStorageOperation search)
        {
            IQueryable<StorageOperation> query = _dbContext.Set<StorageOperation>();

            if ((search?.StorageOperationId).HasValue)
            {
                query = query.Where(o => o.Id == search.StorageOperationId.Value);
            }
            else
            {
                if ((search?.OrderId).HasValue)
                {
                    query = query.Where(o => o.OrderId == search.OrderId.Value);
                }
                if (!(search?.OrderNo).IsNullOrEmpty())
                {
                    query = query.Where(o => o.Order != null && o.Order.OrderNO == search.OrderNo);
                }

                if ((search?.MinCreateTime).HasValue)
                {
                    query = query.Where(o => o.CreateTime >= search.MinCreateTime.Value);
                }

                if ((search?.MaxCreateTime).HasValue)
                {
                    DateTime max = search.MaxCreateTime.Value.AddDays(1);
                    query = query.Where(o => o.CreateTime < max);
                }

                if ((search?.MinUpdateTime).HasValue)
                {
                    query = query.Where(o => o.UpdateTime >= search.MinUpdateTime.Value);
                }

                if ((search?.MaxUpdateTime).HasValue)
                {
                    DateTime max = search.MaxUpdateTime.Value.AddDays(1);
                    query = query.Where(o => o.UpdateTime < max);
                }

                if (!(search?.LogisticName).IsNullOrEmpty())
                {
                    query = query.Where(o => o.Logistic.Name.Contains(search.LogisticName));
                }

                if (!(search?.LogisticNumber).IsNullOrEmpty())
                {
                    query = query.Where(o => o.LogisticNumber.Contains(search.LogisticNumber));
                }

                if (!(search?.CreateBy).IsNullOrEmpty())
                {
                    query = query.Where(o => (o.CreateBy.Id + o.CreateBy.FirstName + o.CreateBy.LastName + o.CreateBy.FirstName + o.CreateBy.Email).Contains(search.CreateBy));
                }

                if (!(search?.UpdateBy).IsNullOrEmpty())
                {
                    query = query.Where(o => (o.UpdateBy.Id + o.UpdateBy.FirstName + o.UpdateBy.LastName + o.UpdateBy.FirstName + o.UpdateBy.Email).Contains(search.UpdateBy));
                }

                if ((search?.Type).HasValue)
                {
                    query = query.Where(o => o.Type == search.Type.Value);
                }

                if (!(search?.WarehouseName).IsNullOrEmpty())
                {
                    query = query.Where(o => o.Warehouse.Name.Contains(search.WarehouseName));
                }
            }

            var total = await query.CountAsync().ConfigureAwait(false);

            var pageSize = (search?.PageSize).GetValueOrDefault(10);
            var pageNumber = (search?.PageNumber).GetValueOrDefault(1) - 1;

            var items = await query
            .OrderByDescending(o => o.UpdateTime)
            .Skip<StorageOperation>(pageNumber * pageSize)
            .Take<StorageOperation>(pageSize)
            .ToListAsync()
            .ConfigureAwait(false);

            return new SearchResult<StorageOperation> { Total = total, Items = items };
        }

        public async Task<StorageOperation> Add(AddStorageOperation add, int operatorId)
        {
            var storageOperation = Mapper.Map<StorageOperation>(add);

            if (storageOperation.Type == StorageOperationType.In)
            {
                var productIds = storageOperation
                    .Details
                    .Select(d => d.ProductId)
                    .ToArray();

                var storages = await _dbContext
                    .Set<ProductStorage>()
                    .Where(s => productIds.Contains(s.ProductId))
                    .ToDictionaryAsync(s => s.ProductId)
                    .ConfigureAwait(false);

                foreach (var detail in storageOperation.Details)
                {
                    ProductStorage storage;
                    if (storages.ContainsKey(detail.ProductId))
                    {
                        storage = storages[detail.ProductId];
                    }
                    else
                    {
                        storage = new ProductStorage
                        {
                            ProductId = detail.ProductId,
                            Current = 0,
                            RowVersion = DateTime.UtcNow
                        };
                        _dbContext.Set<ProductStorage>().Add(storage);
                    }

                    if (storage.Current != detail.OriginalCount)
                    {
                        throw new ConflictException<ProductStorage>(nameof(ProductStorage.Current), detail.OriginalCount);
                    }

                    storage.Current = detail.OriginalCount + detail.OperationCount;
                }
            }
            else
            {
                var order = await _dbContext
                    .Set<Order>()
                    .Include(o => o.Details)
                    .SingleOrDefaultAsync(o => o.Id == storageOperation.OrderId)
                    .ConfigureAwait(false);

                if (order == null)
                {
                    throw new NotFoundException<Order>($"{nameof(Order.Id)}={storageOperation.OrderId.ToString()}");
                }

                var deliveryDetails = add.Details.ToDictionary(d => d.OrderDetailId);
                foreach (var detail in order.Details)
                {
                    if (deliveryDetails.ContainsKey(detail.Id))
                    {
                        detail.Delivered += deliveryDetails[detail.Id].OperationCount.Value;
                        if (detail.Delivered > detail.Count)
                        {
                            throw new ConflictException<StorageOperationDetail>(
                                nameof(StorageOperationDetail.OperationCount),
                                deliveryDetails[detail.Id].OperationCount.Value);
                        }
                    }
                }

                order.LogisticId = add.LogisticId;
                order.LogisticOrderNumber = add.LogisticNumber;
            }

            var now = DateTime.Now;
            storageOperation.CreateTime = now;
            storageOperation.CreatorId = operatorId;
            storageOperation.UpdateTime = now;
            storageOperation.UpdaterId = operatorId;

            _dbContext.Set<StorageOperation>().Add(storageOperation);
            return storageOperation;
        }

        public async Task Update(int id, UpdateStorageOperation update, int operatorId)
        {
            var storageOperation = await this.Get(id).ConfigureAwait(false);
            if (storageOperation == null)
            {
                throw new NotFoundException<StorageOperation>($"{nameof(StorageOperation.Id)}={id.ToString()}");
            }

            Mapper.Map(update, storageOperation);
            storageOperation.UpdaterId = operatorId;
            storageOperation.UpdateTime = DateTime.Now;
        }
    }
}
