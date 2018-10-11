using Allure.Admin.Models;
using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Allure.Common;
using System.Linq.Expressions;
using Allure.Data;
using RefactorThis.GraphDiff;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;

namespace Allure.Admin.Controllers
{
    public class StorageOperationController : AdminControllerBase
    {
        [HttpPost]
        public StorageOperationOutput Create(CreateStorageOperationInput input)
        {
            if (input.Type == StorageOperationType.Out)
            {
                if (!input.OrderId.HasValue)
                {
                    //TODO：为了方便前端测试，出库操作暂时不需要提供订单id
                    //throw new Exception("out storage operation must contain an order id");
                }
                else
                {
                    using (var dbContext = new AllureContext())
                    {
                        var order = dbContext.Set<Order>().SingleOrDefault(o => o.Id == input.OrderId.Value);
                        if (order == null)
                        {
                            throw new Exception(string.Format("invalid order id: {0}", input.OrderId.Value.ToString()));
                        }
                    }
                }
            }

            var operation = new StorageOperation
            {
                Type = input.Type,
                OrderId = input.OrderId,
                WarehouseCode = input.WarehouseCode,
                LogisticCode = input.LogisticCode,
                LogisticOrderNumber = input.LogisticOrderNumber,
                LogisticFee = input.LogisticFee,
                Comment = input.Comment,
                CreateDate = DateTime.Today,
                UpdateDate = DateTime.Today                
            };

            using (var dbContext = new AllureContext())
            {
                var productIds = input.Details.Select(d => d.ProductId).ToArray();
                var balances = dbContext.Set<ProductStorage>()
                    .Where(s => productIds.Contains(s.ProductId))
                    .ToDictionary(s => s.ProductId);

                foreach (var detail in input.Details)
                {
                    operation.Details.Add(new StorageOperationDetail
                    {
                        ProductId = detail.ProductId,
                        CurrentBalance = balances[detail.ProductId].Balance,
                        OperationCount = detail.OperationCount
                    });

                    if (input.Type == StorageOperationType.In)
                    {
                        balances[detail.ProductId].Balance += detail.OperationCount;
                    }
                    else if (input.Type == StorageOperationType.Out)
                    {
                        balances[detail.ProductId].Frozen -= detail.OperationCount;
                    }
                }

                dbContext.Set<StorageOperation>().Add(operation);
                dbContext.SaveChanges();

                return new StorageOperationOutput(operation);
            }
        }

        [HttpPost]
        public StorageOperationOutput Update(UpdateStorageOperationInput input)
        {
            using (var dbContext = new AllureContext())
            {
                var operation = dbContext
                    .Set<StorageOperation>()
                    .Include(s => s.Details)
                    .SingleOrDefault(s => s.Id == input.Id);

                if (operation == null)
                {
                    throw new HttpException(404, string.Format("operation {0} doesn't exist", input.Id.ToString()));
                }

                operation.LogisticFee = input.LogisticFee;
                operation.UpdateDate = DateTime.Today;
                dbContext.SaveChanges();

                return new StorageOperationOutput(operation);
            }
        }

        [HttpPost]
        public SearchStorageOperationOutput Search(SearchStorageOperationInput input)
        {
            using (var dbContext = new AllureContext())
            {
                IQueryable<StorageOperation> query = dbContext
                    .Set<StorageOperation>()
                    .Include(s => s.Details);

                if (input.Id.HasValue)
                {
                    query = query.Where(s => s.Id == input.Id.Value);
                }

                if (input.OrderId.HasValue)
                {
                    query = query.Where(s => s.OrderId == input.OrderId.Value);
                }

                if (input.CreateDate.HasValue)
                {
                    query = query.Where(s => s.CreateDate == input.CreateDate.Value);
                }

                if (input.UpdateDate.HasValue)
                {
                    query = query.Where(s => s.UpdateDate == input.UpdateDate.Value);
                }

                if (!input.LogisticCode.IsNullOrEmpty())
                {
                    query = query.Where(s => s.LogisticCode.Equals(input.LogisticCode));
                }

                if (!input.LogisticOrderNumber.IsNullOrEmpty())
                {
                    query = query.Where(s => s.LogisticOrderNumber.Equals(input.LogisticOrderNumber));
                }

                if (input.Type.HasValue)
                {
                    query = query.Where(s => s.Type == input.Type.Value);
                }

                if (!input.WarehouseCode.IsNullOrEmpty())
                {
                    query = query.Where(s => s.Warehouse.Equals(input.WarehouseCode));
                }

                var result = new SearchStorageOperationOutput();
                result.Count = query.Count();

                var pageSize = input.PageSize.GetValueOrDefault(10);
                var pageNumber = input.PageNumber.GetValueOrDefault(1) - 1;

                result.StorageOperations = query
                    .OrderBy(s => s.Id)
                    .Skip(pageNumber * pageSize)
                    .Take(pageSize)
                    .ToArray()
                    .Select(s => new StorageOperationListOutput(s))
                    .ToArray();

                return result;
            }
        }

        [HttpGet]
        public StorageOperationOutput Id(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var operation = dbContext
                    .Set<StorageOperation>()
                    .Include(s => s.Details)
                    .SingleOrDefault(s => s.Id == id);

                if (operation == null)
                {
                    throw new HttpException(404, string.Format("operation {0} doesn't exist", id.ToString()));
                }

                return new StorageOperationOutput(operation);
            }
        }
    }
}