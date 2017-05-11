using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Allure.Core;
using Allure.Core.Criteria.Locales;
using Allure.Core.Models;
using Allure.Web.Areas.Admin.Models.Locales;
using AutoMapper;

namespace Allure.Web.Areas.Admin.Controllers
{
    public class LocalesController : ApiControllerBase
    {
        public LocalesController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        /// <summary>
        /// get locale by id
        /// </summary>
        /// <param name="id">the id of the locale</param>
        /// <returns></returns>
        public async Task<ViewLocale> Get(int id)
        {
            var locale = await _unitOfWork.Locales.Get(id).ConfigureAwait(false);
            return Mapper.Map<ViewLocale>(locale);
        }

        /// <summary>
        /// get all locales
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ViewLocale>> Get()
        {
            var locales = await _unitOfWork.Locales.GetAll().ConfigureAwait(false);
            return Mapper.Map<IEnumerable<ViewLocale>>(locales);
        }

        /// <summary>
        /// create a new locale
        /// </summary>
        /// <param name="add">the information of the adding locale</param>
        /// <returns>the added locale</returns>
        [ResponseType(typeof(ViewLocale))]
        public async Task<IHttpActionResult> Post([FromBody]AddLocale add)
        {
            var locale = _unitOfWork.Locales.Add(add);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var view = Mapper.Map<ViewLocale>(locale);
            return Ok(view);
        }

        /// <summary>
        /// update an existing locale
        /// </summary>
        /// <param name="id">the id of the locale</param>
        /// <param name="update">the information for update</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Patch(int id, [FromBody]UpdateLocale update)
        {
            await _unitOfWork.Locales.Update(id, update).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// delete an existing locale
        /// </summary>
        /// <param name="id">the id of the deleting locale</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _unitOfWork.Locales.Delete(id).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }
    }
}