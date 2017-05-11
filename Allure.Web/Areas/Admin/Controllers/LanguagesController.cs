using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Allure.Core;
using Allure.Core.Criteria.Languages;
using Allure.Core.Models;
using Allure.Web.Areas.Admin.Models.Languages;
using AutoMapper;

namespace Allure.Web.Areas.Admin.Controllers
{
    public class LanguagesController : ApiControllerBase
    {
        public LanguagesController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        /// <summary>
        /// get language by code
        /// </summary>
        /// <param name="code">the code of the language</param>
        /// <returns></returns>
        [Route("api/languages/{code}")]
        public async Task<ViewLanguage> Get(string code)
        {
            var language = await _unitOfWork.Languages.Get(code).ConfigureAwait(false);
            return Mapper.Map<ViewLanguage>(language);
        }

        /// <summary>
        /// get languages using in the application
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ViewLanguage>> Get()
        {
            var languages = await _unitOfWork.Languages.GetAll().ConfigureAwait(false);
            return Mapper.Map<IEnumerable<ViewLanguage>>(languages);
        }

        /// <summary>
        /// get languages not using, but supported by the server
        /// </summary>
        /// <returns></returns>
        [Route("api/languages/potential")]
        public async Task<IEnumerable<ViewLanguage>> GetPotential()
        {
            var languages = await _unitOfWork.Languages.GetPotential().ConfigureAwait(false);
            return Mapper.Map<IEnumerable<ViewLanguage>>(languages);
        }

        /// <summary>
        /// create a language for the application from the potential languages
        /// </summary>
        /// <param name="add">the information for creation</param>
        [ResponseType(typeof(ViewLanguage))]
        public async Task<IHttpActionResult> Post([FromBody]AddLanguage add)
        {
            var language = await _unitOfWork.Languages.Add(add).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var view = Mapper.Map<ViewLanguage>(language);
            return Ok(view);
        }

        /// <summary>
        /// update a language
        /// </summary>
        /// <param name="code">the code of the language to update</param>
        /// <param name="update">the information for update</param>
        /// <returns></returns>
        [Route("api/languages/{code}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Patch(string code, [FromBody]UpdateLanguage update)
        {
            await _unitOfWork.Languages.Update(code, update).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }
    }
}