using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Allure.Core;
using Allure.Core.Criteria.Warehouses;
using Allure.Core.Models;
using Allure.Web.Areas.Admin.Models.Warehouses;
using AutoMapper;

namespace Allure.Web.Areas.Admin.Controllers
{
    public class WarehousesController : ApiControllerBase
    {
        public WarehousesController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        /// <summary>
        /// get warehouse by code
        /// </summary>
        /// <param name="id">the id of the warehouse</param>
        /// <returns></returns>
        public async Task<ViewWarehouse> Get(int id)
        {
            var warehouse = await _unitOfWork.Warehouses.Get(id).ConfigureAwait(false);
            return Mapper.Map<ViewWarehouse>(warehouse);
        }

        /// <summary>
        /// get all warehouses
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ViewWarehouse>> Get()
        {
            var warehouses = await _unitOfWork.Warehouses.GetAll().ConfigureAwait(false);
            return Mapper.Map<IEnumerable<ViewWarehouse>>(warehouses);
        }

        /// <summary>
        /// create a new warehouse
        /// </summary>
        /// <param name="add">the information of the adding warehouse</param>
        /// <returns>the added warehouse</returns>
        [ResponseType(typeof(ViewWarehouse))]
        public async Task<IHttpActionResult> Post([FromBody]AddWarehouse add)
        {
            var warehouse = _unitOfWork.Warehouses.Add(add);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var view = Mapper.Map<ViewWarehouse>(warehouse);
            return Ok(view);
        }

        /// <summary>
        /// update an existing warehouse
        /// </summary>
        /// <param name="id">the code of the warehouse</param>
        /// <param name="update">the information for update</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Patch(int id, [FromBody]UpdateWarehouse update)
        {
            await _unitOfWork.Warehouses.Update(id, update).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// delete an existing warehouse
        /// </summary>
        /// <param name="id">the id of the deleting warehouse</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _unitOfWork.Warehouses.Delete(id).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }
    }
}