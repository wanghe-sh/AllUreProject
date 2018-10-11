using Allure.Common;
using Allure.Common.Unity;
using Allure.Core.Repositories;
using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Services.EncryptionService;

namespace Allure.Core.Services.UserService
{
    [Autowire(typeof(ISysTextService), Lifetime = Lifetime.Transient)]
    class SysTextService : ISysTextService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISysTextRepository _sysTextRepository;        

        public SysTextService(
            IUnitOfWork unitOfWork,
            ISysTextRepository sysTextRepository)
        {
            _unitOfWork = unitOfWork;
            _sysTextRepository = sysTextRepository;
        }

        string ISysTextService.GetText(string sysTextCode)
        {
            throw new NotImplementedException();
        }
    }
}
