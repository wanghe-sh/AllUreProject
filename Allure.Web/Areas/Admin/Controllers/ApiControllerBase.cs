using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Allure.Core;
using Allure.Core.Models;
using Allure.Web.Utility;

namespace Allure.Web.Areas.Admin.Controllers
{
    public abstract class ApiControllerBase : ApiController
    {
        protected readonly IUnitOfWork _unitOfWork;        

        protected ApiControllerBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }

            base.Dispose(disposing);
        }

        protected int UserId
        {
            get
            {
                return (this.User.Identity as AllureIdentity).User.Id;
            }
        }
    }
}