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
using Allure.Core.Criteria.Products;
using Allure.Core.Models;
using Allure.Web.Areas.Admin.Models.Products;
using AutoMapper;

namespace Allure.Web.Areas.Admin.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        public ProductsController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        /// <summary>
        /// get product by id
        /// </summary>
        /// <param name="id">the id of the product</param>
        /// <returns></returns>
        public async Task<ViewProduct> Get(int id)
        {
            var product = await _unitOfWork.Products.Get(id).ConfigureAwait(false);
            return Mapper.Map<ViewProduct>(product);
        }

        /// <summary>
        /// get product by product number
        /// </summary>
        /// <param name="number">the product number</param>
        /// <returns></returns>
        [Route("api/products/NO-{number}")]
        public async Task<ViewProductStorage> Get(string number)
        {
            var product = await _unitOfWork.Products.Get(number).ConfigureAwait(false);
            return Mapper.Map<ViewProductStorage>(product);
        }

        /// <summary>
        /// search products by conditions
        /// </summary>
        /// <returns>the total count and paged records</returns>
        public async Task<SearchResult<ViewSearchProduct>> Get([FromUri]SearchProduct search)
        {
            var result = await _unitOfWork.Products.Search(search).ConfigureAwait(false);
            return Mapper.Map<SearchResult<ViewSearchProduct>>(result);
        }

        /// <summary>
        /// search product storages by conditions
        /// </summary>
        /// <returns>the total count and paged records</returns>
        [Route("api/products/storage")]
        public async Task<SearchResult<ViewProductStorage>> GetStorages([FromUri]SearchProduct search)
        {
            var result = await _unitOfWork.Products.Search(search).ConfigureAwait(false);
            return Mapper.Map<SearchResult<ViewProductStorage>>(result);
        }

        /// <summary>
        /// create a new product
        /// </summary>
        /// <param name="add">the information of the adding product</param>
        /// <returns>the added product</returns>
        [ResponseType(typeof(ViewProduct))]
        public async Task<IHttpActionResult> Post([FromBody]AddProduct add)
        {
            var product = await _unitOfWork.Products.Add(add).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var view = Mapper.Map<ViewProduct>(product);
            return Ok(view);
        }

        /// <summary>
        /// update an existing product
        /// </summary>
        /// <param name="id">the id of the product</param>
        /// <param name="update">the information for update</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Patch(int id, [FromBody]UpdateProduct update)
        {
            await _unitOfWork.Products.Update(id, update).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// delete an existing product
        /// </summary>
        /// <param name="id">the id of the deleting product</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _unitOfWork.Products.Delete(id).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }
    }
}