using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common.Unity;
using Allure.Core.Exceptions;
using Allure.Core.Models;
using Allure.Core.Repositories;
using Allure.Common.Validations;
using AutoMapper;
using Allure.Core.Criteria.CheckAddresses;
using Allure.Core.Criteria.Brands;

namespace Allure.Data.Repositories
{
    [Autowire(typeof(ICheckAddressRepository), Lifetime.PerResolve)]
    class CheckAddressRepository : ICheckAddressRepository
    {
        private readonly IDbContext _dbContext;

        public CheckAddressRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<CheckAddress> Get(int id)
        {
            return await _dbContext
                .Set<CheckAddress>()
                .SingleOrDefaultAsync(b => b.Id == id )
                .ConfigureAwait(false);
        }

        public async Task Update(int id, UpdateCheckAddress update)
        {
            var checkAddress = await this.Get(id).ConfigureAwait(false);
            if (checkAddress == null)
            {
                throw new NotFoundException<CheckAddress>($"{nameof(CheckAddress.Id)}={id.ToString()}");
            }

            Mapper.Map(update, checkAddress);            
        }

       
    }
}
