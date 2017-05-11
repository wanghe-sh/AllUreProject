using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Allure.Core;
using Allure.Core.Criteria;
using Allure.Core.Criteria.Orders;
using Allure.Web.Areas.Admin.Models.Orders;
using AutoMapper;

namespace Allure.Web.Areas.Admin.Controllers
{
    public class OrdersController : ApiControllerBase
    {
        public OrdersController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        /// <summary>
        /// get order by id
        /// </summary>
        /// <param name="id">the id of the order</param>
        /// <returns></returns>
        public async Task<ViewOrder> Get(int id)
        {
            var order = await _unitOfWork.Orders.Get(id).ConfigureAwait(false);
            return Mapper.Map<ViewOrder>(order);
        }

        /// <summary>
        /// search orders by conditions
        /// </summary>
        /// <returns>the total count and paged records</returns>
        public async Task<SearchResult<ViewSearchOrder>> Get([FromUri]SearchOrder search)
        {
            var result = await _unitOfWork.Orders.Search(search).ConfigureAwait(false);
            return Mapper.Map<SearchResult<ViewSearchOrder>>(result);
        }

        /// <summary>
        /// update an existing order
        /// </summary>
        /// <param name="id">the id of the order</param>
        /// <param name="update">the information for update</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Patch(int id, [FromBody]UpdateOrder update)
        {
            await _unitOfWork.Orders.Update(id, update).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }
    }
}