using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Allure.Core;
using Allure.Core.Criteria.Logistics;
using Allure.Core.Models;
using Allure.Web.Areas.Admin.Models.Logistics;
using AutoMapper;

namespace Allure.Web.Areas.Admin.Controllers
{
    public class LogisticsController : ApiControllerBase
    {
        public LogisticsController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        /// <summary>
        /// get logistic by id
        /// </summary>
        /// <param name="id">the id of the logistic</param>
        /// <returns></returns>
        public async Task<ViewLogistic> Get(int id)
        {
            var logistic = await _unitOfWork.Logistics.Get(id).ConfigureAwait(false);
            return Mapper.Map<ViewLogistic>(logistic);
        }

        /// <summary>
        /// get all logistics
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ViewLogistic>> Get()
        {
            var logistics = await _unitOfWork.Logistics.GetAll().ConfigureAwait(false);
            return Mapper.Map<IEnumerable<ViewLogistic>>(logistics);
        }

        /// <summary>
        /// create a new logistic
        /// </summary>
        /// <param name="add">the information of the adding logistic</param>
        /// <returns>the added logistic</returns>
        [ResponseType(typeof(ViewLogistic))]
        public async Task<IHttpActionResult> Post([FromBody]AddLogistic add)
        {
            var logistic = _unitOfWork.Logistics.Add(add);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var view = Mapper.Map<ViewLogistic>(logistic);
            return Ok(view);
        }

        /// <summary>
        /// update an existing logistic
        /// </summary>
        /// <param name="id">the id of the logistic</param>
        /// <param name="update">the information for update</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Patch(int id, [FromBody]UpdateLogistic update)
        {
            await _unitOfWork.Logistics.Update(id, update).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// delete an existing logistic
        /// </summary>
        /// <param name="id">the id of the deleting logistic</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _unitOfWork.Logistics.Delete(id).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }
    }
}