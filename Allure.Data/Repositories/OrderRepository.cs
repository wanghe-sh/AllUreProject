using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common;
using Allure.Common.Unity;
using Allure.Core.Criteria;
using Allure.Core.Criteria.Orders;
using Allure.Core.Exceptions;
using Allure.Core.Models;
using Allure.Core.Repositories;
using Allure.Common.Validations;
using AutoMapper;

namespace Allure.Data.Repositories
{
    [Autowire(typeof(IOrderRepository), Lifetime.PerResolve)]
    class OrderRepository : IOrderRepository
    {
        private readonly IDbContext _dbContext;

        public OrderRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> Get(int id)
        {
            return await _dbContext
                .Set<Order>()
                .SingleOrDefaultAsync(o => o.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<SearchResult<Order>> Search(SearchOrder search)
        {
            IQueryable<Order> query = _dbContext.Set<Order>();

            if (!string.IsNullOrEmpty(search.OrderNo))
            {
                query = query.Where(o => o.OrderNO.Contains(search.OrderNo));
            }
            else
            {
                if ((search?.Status).HasValue)
                {
                    query = query.Where(o => o.Status == search.Status.Value);
                }

                if (!(search?.Customer).IsNullOrEmpty())
                {
                    int customerId;
                    if (int.TryParse(search.Customer, out customerId))
                    {
                        query = query.Where(o => o.CustomerId == customerId
                            || (o.Customer.FirstName + o.Customer.LastName + o.Customer.FirstName).Contains(search.Customer));
                    }
                    else
                    {
                        query = query.Where(o => (o.Customer.FirstName + o.Customer.LastName + o.Customer.FirstName).Contains(search.Customer));
                    }
                }

                if ((search?.MinTotal).HasValue)
                {
                    query = query.Where(o => o.Details.Sum(d => d.Product.Price * d.Count - d.Discount) - o.Discount >= search.MinTotal.Value);
                }

                if ((search?.MaxTotal).HasValue)
                {
                    query = query.Where(o => o.Details.Sum(d => d.Product.Price * d.Count - d.Discount) - o.Discount <= search.MaxTotal.Value);
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
            }

            var total = await query.CountAsync().ConfigureAwait(false);

            var orderBy = nameof(Order.Id);

            if ((search?.SortBy).HasValue)
            {
                orderBy = $"{search.SortBy.ToString()} {(search.Descending ? Sort.Descending : Sort.Ascending)}, {orderBy}";
            }

            var pageSize = (search?.PageSize).GetValueOrDefault(10);
            var pageNumber = (search?.PageNumber).GetValueOrDefault(1) - 1;

            var items = await query
                .OrderBy(orderBy)
                .Skip<Order>(pageNumber * pageSize)
                .Take<Order>(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);

            return new SearchResult<Order> { Total = total, Items = items };
        }

        public async Task Update(int id, UpdateOrder update)
        {
            var order = await this.Get(id).ConfigureAwait(false);

            if (order == null)
            {
                throw new NotFoundException<Order>($"{nameof(Order.Id)}={id.ToString()}");
            }

            Mapper.Map(update, order);
                        
            var productIds = update.Details
                .Select(d => d.ProductId.Value)
                .Concat(order.Details.Select(d => d.ProductId))
                .ToArray();

            var storages = await _dbContext
                .Set<ProductStorage>()
                .Where(s => productIds.Contains(s.ProductId))
                .ToDictionaryAsync(s => s.ProductId)
                .ConfigureAwait(false);

            foreach (var nonExistId in productIds.Except(storages.Keys))
            {
                var storage = new ProductStorage
                {
                    ProductId = nonExistId,
                    Current = 0,
                    Frozen = 0,
                    RowVersion = DateTime.UtcNow
                };

                _dbContext.Set<ProductStorage>().Add(storage);
                storages.Add(nonExistId, storage);
            }

            var existingDetails = update.Details
                .Where(d => d.Id.HasValue)
                .ToDictionary(d => d.Id.Value);

            foreach (var detail in order.Details.ToArray())
            {
                UpdateOrderDetail updateDetail;
                decimal deltaFrozen;

                if (existingDetails.TryGetValue(detail.Id, out updateDetail) && updateDetail.Count > 0)
                {
                    deltaFrozen = updateDetail.Count.Value - detail.Count;
                    _dbContext.Entry(detail).CurrentValues.SetValues(updateDetail);
                    
                }
                else
                {
                    deltaFrozen = -detail.Count;
                    _dbContext.Entry(detail).State = EntityState.Deleted;
                }
                
                storages[detail.ProductId].Frozen += deltaFrozen;
                storages[detail.ProductId].Current -= deltaFrozen;
            }

            foreach (var added in update.Details.Where(d => !d.Id.HasValue))
            {
                var newDetail = Mapper.Map<OrderDetail>(added);
                order.Details.Add(newDetail);
                storages[newDetail.ProductId].Frozen += newDetail.Count;
                storages[newDetail.ProductId].Current -= newDetail.Count;
            }

            order.UpdateTime = DateTime.Now;
        }
    }
}
