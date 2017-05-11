using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Allure.Core;
using Allure.Core.Criteria.Brands;
using Allure.Core.Models;
using Allure.Web.Areas.Admin.Models.Brands;
using AutoMapper;

namespace Allure.Web.Areas.Admin.Controllers
{
    public class BrandsController : ApiControllerBase
    {
        public BrandsController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        /// <summary>
        /// get brand by id
        /// </summary>
        /// <param name="id">the id of the brand</param>
        /// <returns></returns>        
        public async Task<ViewBrand> Get(int id)
        {
            var brand = await _unitOfWork.Brands.Get(id).ConfigureAwait(false);
            return Mapper.Map<ViewBrand>(brand);
        }

        /// <summary>
        /// get brand by name
        /// </summary>
        /// <param name="name">the name of the brand</param>
        /// <returns></returns>
        [Route("api/brands/NAME-{name}")]
        public async Task<ViewBrand> Get(string name)
        {
            var brand = await _unitOfWork.Brands.Get(name).ConfigureAwait(false);
            return Mapper.Map<ViewBrand>(brand);
        }

        /// <summary>
        /// get all brands
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ViewBrand>> Get()
        {
            var brands = await _unitOfWork.Brands.GetAll().ConfigureAwait(false);
            return Mapper.Map<IEnumerable<ViewBrand>>(brands);
        }

        /// <summary>
        /// create a new brand
        /// </summary>
        /// <param name="add">the information of the adding brand</param>
        /// <returns>the added brand</returns>
        [ResponseType(typeof(ViewBrand))]
        public async Task<IHttpActionResult> Post([FromBody]AddBrand add)
        {
            var brand = _unitOfWork.Brands.Add(add);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var view = Mapper.Map<ViewBrand>(brand);
            return Ok(view);
        }

        /// <summary>
        /// update an existing brand
        /// </summary>
        /// <param name="id">the id of the brand</param>
        /// <param name="update">the information for update</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Patch(int id, [FromBody]UpdateBrand update)
        {            
            await _unitOfWork.Brands.Update(id, update).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// delete an existing brand
        /// </summary>
        /// <param name="id">the id of the deleting brand</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _unitOfWork.Brands.Delete(id).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }
    }
}