using Allure.Common.Unity;
using Allure.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.SubscribedMails;
using Allure.Core.Models;
using AutoMapper;

namespace Allure.Data.Repositories
{
    [Autowire(typeof(ISubscribedMailRepository), Lifetime.PerResolve)]
    class SubscribedMailRepository : ISubscribedMailRepository
    {
        private readonly IDbContext _dbContext;

        public SubscribedMailRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SubscribedMail Add(AddSubscribedMail add)
        {
            var subscribeMail = Mapper.Map<SubscribedMail>(add);
            _dbContext.Set<SubscribedMail>().Add(subscribeMail);
            return subscribeMail;
        }
    }
}
