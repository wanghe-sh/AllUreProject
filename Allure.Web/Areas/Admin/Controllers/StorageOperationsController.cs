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
using Allure.Core.Criteria.StorageOperations;
using Allure.Web.Areas.Admin.Models.StorageOperations;
using AutoMapper;

namespace Allure.Web.Areas.Admin.Controllers
{
    public class StorageOperationsController : ApiControllerBase
    {
        public StorageOperationsController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        /// <summary>
        /// get storage operation by id
        /// </summary>
        /// <param name="id">the id of the storage operation</param>
        /// <returns></returns>
        public async Task<ViewStorageOperation> Get(int id)
        {
            var storageOperation = await _unitOfWork.StorageOperations.Get(id).ConfigureAwait(false);
            return Mapper.Map<ViewStorageOperation>(storageOperation);
        }

        /// <summary>
        /// search storage conditions by conditions
        /// </summary>
        /// <returns>the total count and paged records</returns>
        public async Task<SearchResult<ViewSearchStorageOperation>> Get([FromUri]SearchStorageOperation search)
        {
            var result = await _unitOfWork.StorageOperations.Search(search).ConfigureAwait(false);
            return Mapper.Map<SearchResult<ViewSearchStorageOperation>>(result);
        }

        /// <summary>
        /// create a new storage operation
        /// </summary>
        /// <param name="add">the information of the adding storage operation</param>
        /// <returns>the added storage operation's id</returns>        
        public async Task<int> Post([FromBody]AddStorageOperation add)
        {
            var storageOperation = await _unitOfWork.StorageOperations.Add(add, this.UserId).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return storageOperation.Id;
        }

        /// <summary>
        /// update an existing storage operation
        /// </summary>
        /// <param name="id">the id of the storage operation</param>
        /// <param name="update">the information for update</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Patch(int id, [FromBody]UpdateStorageOperation update)
        {
            await _unitOfWork.StorageOperations.Update(id, update, this.UserId).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }
    }
}