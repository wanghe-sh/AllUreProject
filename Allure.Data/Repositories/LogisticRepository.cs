using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common.Unity;
using Allure.Core.Criteria.Logistics;
using Allure.Core.Exceptions;
using Allure.Core.Models;
using Allure.Core.Repositories;
using Allure.Common.Validations;
using AutoMapper;

namespace Allure.Data.Repositories
{
    [Autowire(typeof(ILogisticRepository), Lifetime.PerResolve)]
    class LogisticRepository : ILogisticRepository
    {
        private readonly IDbContext _dbContext;

        public LogisticRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Logistic Add(AddLogistic add)
        {
            var logistic = Mapper.Map<Logistic>(add);
            _dbContext.Set<Logistic>().Add(logistic);
            return logistic;
        }

        public async Task Delete(int id)
        {
            var logistic = await this.Get(id).ConfigureAwait(false);
            if (logistic == null)
            {
                throw new NotFoundException<Logistic>($"{nameof(Logistic.Id)}={id.ToString()}");
            }

            logistic.Deleted = true;
        }

        public async Task<Logistic> Get(int id)
        {
            return await _dbContext
                .Set<Logistic>()
                .SingleOrDefaultAsync(l => l.Id == id && !l.Deleted)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Logistic>> GetAll()
        {
            return await _dbContext
                .Set<Logistic>()
                .Where(l => !l.Deleted)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task Update(int id, UpdateLogistic update)
        {
            var logistic = await this.Get(id).ConfigureAwait(false);
            if (logistic == null)
            {
                throw new NotFoundException<Logistic>($"{nameof(Logistic.Id)}={id.ToString()}");
            }

            Mapper.Map(update, logistic);
        }
    }
}
