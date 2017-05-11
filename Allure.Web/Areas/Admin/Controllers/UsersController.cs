using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Allure.Core;
using Allure.Core.Criteria;
using Allure.Core.Criteria.Users;
using Allure.Web.Areas.Admin.Models.Users;
using AutoMapper;

namespace Allure.Web.Areas.Admin.Controllers
{
    public class UsersController : ApiControllerBase
    {
        public UsersController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { }

        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <returns></returns>
        public async Task<ViewUser> Get(int id)
        {
            var user = await _unitOfWork.Users.Get(id).ConfigureAwait(false);
            return Mapper.Map<ViewUser>(user);
        }

        /// <summary>
        /// search users by conditions
        /// </summary>
        /// <returns>the total count and paged records</returns>
        public async Task<SearchResult<ViewSearchUser>> Get([FromUri]SearchUser search)
        {
            var result = await _unitOfWork.Users.Search(search).ConfigureAwait(false);
            return Mapper.Map<SearchResult<ViewSearchUser>>(result);
        }

        /// <summary>
        /// create a user
        /// </summary>
        /// <param name="add">the information of the adding user</param>
        /// <returns>the added user</returns>
        [ResponseType(typeof(ViewUser))]
        public async Task<IHttpActionResult> Post([FromBody]AddUser add)
        {
            var user = _unitOfWork.Users.Add(add);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var view = Mapper.Map<ViewUser>(user);
            return Ok(view);
        }

        /// <summary>
        /// update a user
        /// </summary>
        /// <param name="id">the id of the user to update</param>
        /// <param name="update">the information for update</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Patch(int id, [FromBody]UpdateUser update)
        {
            await _unitOfWork.Users.Update(id, update).ConfigureAwait(false);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }

        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <returns></returns>
        public async Task<ViewUser> Get([FromUri]string currentuser)
        {
            var user = await _unitOfWork.Users.Get(Convert.ToInt16(this.User.Identity.Name)).ConfigureAwait(false);
            return Mapper.Map<ViewUser>(user);
        }
    }
}