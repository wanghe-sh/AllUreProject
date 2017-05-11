using Allure.Data;
using Allure.Core.Models;
using Allure.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Allure.Web.Models;

namespace Allure.Web.Controllers
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

                string today = System.DateTime.Today.ToString("yyyyMMdd");
                // get order NO. 20160601001
                string orderNO = context.Set<Order>().Where(o => o.OrderNO.Contains(today)).Select(o => o.OrderNO).Max();
                if (string.IsNullOrEmpty(orderNO))
                {
                    orderNO = today + "001";
                }
                else
                {
                    orderNO = today +    (Convert.ToInt32(orderNO.Substring(9)) + 1).ToString().PadLeft(3,'0');
                }

                var details = input.Details.Where(d => d.Count > 0).Select(d => new OrderDetail
                {
                    ProductId = d.ProductId,
                    Count = d.Count,
                    Discount = 0m
                });

                var order = new Order
                {
                    OrderNO = orderNO,
                    WillCheck = input.WillCheck,
                    CheckTime = input.WillCheck ? input.CheckTime : null,
                    CheckContact = input.CheckContact,
                    CheckAddress = input.CheckAddress,
                    Details = details.ToArray(),
                    Status = OrderStatus.ToBeContact,
                    CustomerId = this.Identity.User.Id,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                };

                var delivery = this.Identity.User.Deliveries.FirstOrDefault();
                if (delivery == null)
                {
                    delivery = new UserDelivery();
                    delivery.UserId = this.Identity.User.Id;
                }
                delivery.Receiver = input.ReceiverName;
                delivery.Address = input.ReceiverAddress;
                delivery.PostCode = input.ReceiverPostCode;
                delivery.Phone = input.ReceiverContact;
                order.Delivery = delivery;

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
                    //.Include(o => o.Details)
                    .Include(o => o.Delivery)
                    .SingleOrDefault(o => o.Id == input.Id);

                if (order == null)
                {
                    throw new Exception(string.Format("order {0} doesn't exist", input.Id.ToString()));
                }

                //var ids = input.Details.Select(d => d.ProductId).ToArray();
                //var products = context.Set<Product>().Where(p => ids.Contains(p.Id)).ToDictionary(p => p.Id);

                //var details = input.Details.Where(d => d.Count > 0).Select(d => new OrderDetail
                //{
                //    ProductId = d.ProductId,
                //    Count = d.Count,
                //    Discount = 0m
                //});

                order.WillCheck = input.WillCheck;
                order.CheckTime = input.WillCheck ? input.CheckTime : null;
                order.CheckContact = input.CheckContact;
                order.CheckAddress = input.CheckAddress;
                order.Delivery.Receiver = input.ReceiverName;
                order.Delivery.Address = input.ReceiverAddress;
                order.Delivery.PostCode = input.ReceiverPostCode;
                order.Delivery.Phone = input.ReceiverContact;

                //order.Details = details.ToList();
                //order. = order.Details.Sum(d => d.RealCharge);
                //order.RealCharge = order.OriginalRealCharge;
                order.UpdateTime = DateTime.Now;

                //context.UpdateGraph(order, o => o.OwnedCollection(oo => oo.Details));
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
                    .Include(o => o.Details)
                    .Include(o => o.Logistic)
                    .Include("Details.Product.Localizations")
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

    public  class ViewNamesClass
    {
        public readonly static string Detail = "Detail";
        public readonly static string Index = "Index";
        public readonly static string List = "List";
        public readonly static string Submit = "Submit";


    }
}
