using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Allure.Core;
using Allure.Core.Criteria.CheckAddresses;
using Allure.Core.Models;
using Allure.Web.Areas.Admin.Models.CheckAddresses;
using AutoMapper;

namespace Allure.Web.Areas.Admin.Controllers
{
    public class CheckAddressesController : ApiControllerBase
    {
        public CheckAddressesController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        /// <summary>
        /// get checkAddress by id
        /// </summary>
        /// <param name="id">the id of the checkAddress</param>
        /// <returns></returns>        
        public async Task<ViewCheckAddress> Get(int id)
        {
            var checkAddress = await _unitOfWork.CheckAddresses.Get(id).ConfigureAwait(false);
            return Mapper.Map<ViewCheckAddress>(checkAddress);
        }

        
        
        /// <summary>
        /// update an existing checkAddress
        /// </summary>
        /// <param name="id">the id of the checkAddress</param>
        /// <param name="update">the information for update</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Patch(int id, [FromBody]UpdateCheckAddress update)
        {            
            await _unitOfWork.CheckAddresses.Update(id, update).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

       
    }
}