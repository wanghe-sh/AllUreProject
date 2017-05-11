using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Allure.Core;
using Allure.Core.Criteria.Categories;
using Allure.Core.Models;
using Allure.Web.Areas.Admin.Models.Categories;
using AutoMapper;

namespace Allure.Web.Areas.Admin.Controllers
{
    public class CategoriesController : ApiControllerBase
    {
        public CategoriesController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        /// <summary>
        /// get category by id
        /// </summary>
        /// <param name="id">the id of the category</param>
        /// <returns></returns>
        public async Task<ViewCategory> Get(int id)
        {
            var category = await _unitOfWork.Categories.Get(id).ConfigureAwait(false);
            return Mapper.Map<ViewCategory>(category);
        }

        /// <summary>
        /// get all categories
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ViewCategory>> Get()
        {
            var categories = await _unitOfWork.Categories.GetRoots().ConfigureAwait(false);
            return Mapper.Map<IEnumerable<ViewCategory>>(categories);
        }

        /// <summary>
        /// create a new category
        /// </summary>
        /// <param name="add">the information of the adding category</param>
        /// <returns>the added category</returns>
        [ResponseType(typeof(ViewCategory))]
        public async Task<IHttpActionResult> Post([FromBody]AddCategory add)
        {
            var category = _unitOfWork.Categories.Add(add);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var view = Mapper.Map<ViewCategory>(category);
            return Ok(view);
        }

        /// <summary>
        /// update an existing category
        /// </summary>
        /// <param name="id">the id of the category</param>
        /// <param name="update">the information for update</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Patch(int id, [FromBody]UpdateCategory update)
        {
            await _unitOfWork.Categories.Update(id, update).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// delete an existing category
        /// </summary>
        /// <param name="id">the id of the deleting category</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _unitOfWork.Categories.Delete(id).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }
    }
}