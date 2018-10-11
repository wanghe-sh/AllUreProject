using Allure.Data;
using Allure.Core.Models;
using Allure.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using RefactorThis.GraphDiff;

namespace Allure.UI.Controllers
{
    [Authorize]
    public partial class OrderController : MainControllerBase
    {
        public virtual ActionResult Index()
        {
            return View(CreateViewModel());
        }

        [HttpGet]
        public virtual ActionResult Submit()
        {
            return View(CreateViewModel());
        }

        [HttpPost]
        public virtual ActionResult Submit(OrderInput input)
        {
            using (var context = new AllureContext())
            {
                var ids = input.Details.Select(d => d.ProductId).ToArray();
                var products = context.Set<Product>().Where(p => ids.Contains(p.Id)).ToDictionary(p => p.Id);

                var details = input.Details.Where(d => d.Count > 0).Select(d => new OrderDetail
                {
                    ProductId = d.ProductId,
                    Count = d.Count,
                    Discount = 0m
                });

                var order = new Order
                {
                    WillCheck = input.WillCheck,
                    CheckTime = input.WillCheck ? input.CheckTime : null,
                    CheckContact = input.CheckContact,
                    CheckAddress = input.CheckAddress,
                    ReceiverName = input.ReceiverName,
                    ReceiverAddress = input.ReceiverAddress,
                    ReceiverPostCode = input.ReceiverPostCode,
                    ReceiverContact = input.ReceiverContact,
                    Details = details.ToArray(),
                    Status = OrderStatus.New,
                    CustomerId = this.Identity.User.Id,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };

                context.Set<Order>().Add(order);
                context.SaveChanges();

                return Json("ok");
            }
        }

        [HttpPost]
        public virtual ActionResult Save(OrderInput input)
        {
            using (var context = new AllureContext())
            {
                var order = context.Set<Order>()
                    .Include(o => o.Details)
                    .AsNoTracking()
                    .SingleOrDefault(o => o.Id == input.Id);

                if (order == null)
                {
                    throw new Exception(string.Format("order {0} doesn't exist", input.Id.ToString()));
                }

                var ids = input.Details.Select(d => d.ProductId).ToArray();
                var products = context.Set<Product>().Where(p => ids.Contains(p.Id)).ToDictionary(p => p.Id);

                var details = input.Details.Where(d => d.Count > 0).Select(d => new OrderDetail
                {
                    ProductId = d.ProductId,
                    Count = d.Count,
                    Discount = 0m
                });

                order.WillCheck = input.WillCheck;
                order.CheckTime = input.WillCheck ? input.CheckTime : null;
                order.CheckContact = input.CheckContact;
                order.CheckAddress = input.CheckAddress;
                order.ReceiverName = input.ReceiverName;
                order.ReceiverAddress = input.ReceiverAddress;
                order.ReceiverPostCode = input.ReceiverPostCode;
                order.ReceiverContact = input.ReceiverContact;
                order.Details = details.ToList();
                order.OriginalRealCharge = order.Details.Sum(d => d.RealCharge);
                order.RealCharge = order.OriginalRealCharge;
                order.UpdateTime = DateTime.Now;

                context.UpdateGraph(order, o => o.OwnedCollection(oo => oo.Details));
                context.SaveChanges();

                return Json("ok");
            }
        }
        
        public virtual ActionResult Detail(int id)
        {
            using (var dbContext = new AllureContext())
            {
                var order = dbContext
                    .Set<Order>()
                    .Include("Details.Product.Localized")
                    .SingleOrDefault(o => o.Id == id);

                if (order == null)
                {
                    throw new HttpException(404, string.Format("Order {0} doesn't exist", id.ToString()));
                }

                var model = new OrderViewOutput(order, this.LanguageCode);
                return View(CreateViewModel(model));
            }
        }

        public virtual ActionResult List()
        {
            using (var dbContext = new AllureContext())
            {
                var orders = dbContext
                    .Set<Order>()
                    .Include(o => o.Details)
                    .Where(o => o.CustomerId == this.Identity.User.Id)
                    .ToArray()
                    .Select(o => new OrderOutput(o))
                    .ToArray();

                return View(CreateViewModel(orders));
            }
        }
    }
}
