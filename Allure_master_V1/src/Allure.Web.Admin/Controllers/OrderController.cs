using Allure.Data;
using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using RefactorThis.GraphDiff;
using System.Data.Entity;
using Allure.Common;
using Allure.Admin.Models;

namespace Allure.Admin.Controllers
{
    public class OrderController : AdminControllerBase
    {
        [HttpPost]
        public SearchOrderOutput Search(SearchOrderInput input)
        {
            using (var dbContext = new AllureContext())
            {
                IQueryable<Order> query = dbContext
                    .Set<Order>()
                    .Include(o => o.Details)
                    .Include(o => o.Customer);

                if (input.Id.HasValue)
                {
                    query = query.Where(o => o.Id == input.Id);
                }

                if (input.Status.HasValue)
                {
                    query = query.Where(o => o.Status == input.Status.Value);
                }

                if (input.MinTotalPrice.HasValue)
                {
                    query = query.Where(o => o.OriginalRealCharge >= input.MinTotalPrice.Value);
                }

                if (input.MaxTotalPrice.HasValue)
                {
                    query = query.Where(o => o.OriginalRealCharge <= input.MaxTotalPrice.Value);
                }

                if (!input.CustomerName.IsNullOrEmpty())
                {
                    query = query.Where(o => o.Customer.FirstName.Contains(input.CustomerName) 
                        || o.Customer.LastName.Contains(input.CustomerName));
                }

                if (input.MinCreateTime.HasValue)
                {
                    query = query.Where(o => o.CreateTime >= input.MinCreateTime.Value);
                }

                if (input.MaxCreateTime.HasValue)
                {
                    query = query.Where(o => o.CreateTime <= input.MaxCreateTime.Value);
                }

                if (input.MinUpdateTime.HasValue)
                {
                    query = query.Where(o => o.UpdateTime >= input.MinUpdateTime.Value);
                }

                if (input.MaxUpdateTime.HasValue)
                {
                    query = query.Where(o => o.UpdateTime <= input.MaxUpdateTime.Value);
                }

                var result = new SearchOrderOutput();
                result.Count = query.Count();

                var pageSize = input.PageSize.GetValueOrDefault(10);
                var pageNumber = input.PageNumber.GetValueOrDefault(1) - 1;

                result.Orders = query
                    .OrderBy(o => o.Id)
                    .Skip(pageNumber * pageSize)
                    .Take(pageSize)
                    .ToArray()
                    .Select(o => new OrderOutput(o))
                    .ToArray();

                return result;
            }
        }

        [HttpGet]
        public OrderOutput Id(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var order = dbContext.Set<Order>().SingleOrDefault(o => o.Id == id);

                if (order == null)
                {
                    throw new HttpException(404, string.Format("order {0} doesn't exist", id.ToString()));
                }

                return new OrderOutput(order);
            }
        }

        //[HttpPost]
        //public OrderOutput Create(OrderInput input)
        //{
        //    var order = new Order
        //    {
        //        CustomerId = input.CustomerId,
        //        Status = input.Status,
        //        CreateTime = DateTime.Now,
        //        UpdateTime = DateTime.Now,
        //        Details = input.Details.Select(d => new OrderDetail
        //        {
        //            ProductId = d.ProductId,
        //            Price = d.Price,
        //            Count = d.Count
        //        }).ToArray(),
        //        TotalPrice = input.Details.Sum(d => d.Price * d.Count)
        //    };

        //    using (var dbContext = new AllureContext())
        //    {
        //        dbContext.Set<Order>().Add(order);
        //        dbContext.SaveChanges();

        //        return Id(order.Id);
        //    }
        //}

        [HttpPost]
        public OrderOutput Update(OrderInput input)
        {
            using (var dbContext = new AllureContext())
            {
                var order = dbContext.Set<Order>().SingleOrDefault(o => o.Id == input.Id);

                if (order == null)
                {
                    throw new HttpException(404, string.Format("order {0} doesn't exist", input.Id.ToString()));
                }

                order.Status = input.Status;
                order.WillCheck = input.WillCheck;
                order.CheckAddress = input.CheckAddress;
                order.CheckTime = input.CheckTime;
                order.CheckContact = input.CheckContact;
                order.LogisticCode = input.LogisticCode;
                order.LogisticOrderNumber = input.LogisticOrderNumber;
                order.Deposit = input.Deposit;                
                order.DepositReceipt = input.DepositReceipt;
                order.Remaining = input.Remaining;
                order.RemainingReceipt = input.RemainingReceipt;
                order.UpdateTime = DateTime.Now;
                if (order.Deposit.HasValue)
                {
                    order.RealCharge = order.OriginalRealCharge - order.Deposit.Value;
                }

                dbContext.SaveChanges();

                return Id(order.Id);
            }
        }

        [HttpPost]
        public void Delete(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var order = dbContext.Set<Order>().SingleOrDefault(o => o.Id == id);

                if (order == null)
                {
                    throw new HttpException(404, string.Format("order {0} doesn't exist", id.ToString()));
                }

                dbContext.Set<OrderDetail>().RemoveRange(order.Details);
                dbContext.Entry(order).State = EntityState.Deleted;
                dbContext.SaveChanges();
            }
        }
    }
}
