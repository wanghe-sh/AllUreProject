using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common.Unity;
using Allure.Core.Criteria.Locales;
using Allure.Core.Exceptions;
using Allure.Core.Models;
using Allure.Core.Repositories;
using Allure.Common.Validations;
using AutoMapper;

namespace Allure.Data.Repositories
{
    [Autowire(typeof(ILocaleRepository), Lifetime.PerResolve)]
    class LocaleRepository : ILocaleRepository
    {
        private readonly IDbContext _dbContext;

        public LocaleRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Locale Add(AddLocale add)
        {
            var locale = Mapper.Map<Locale>(add);
            _dbContext.Set<Locale>().Add(locale);
            return locale;
        }

        public async Task Delete(int id)
        {
            var locale = await this.Get(id).ConfigureAwait(false);
            if (locale == null)
            {
                throw new NotFoundException<Locale>($"{nameof(Locale.Id)}={id.ToString()}");
            }

            locale.Deleted = true;
        }

        public async Task<Locale> Get(int id)
        {
            return await _dbContext
                .Set<Locale>()
                .SingleOrDefaultAsync(l => l.Id == id && !l.Deleted)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Locale>> GetAll()
        {
            return await _dbContext
                .Set<Locale>()
                .Where(l => !l.Deleted)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task Update(int id, UpdateLocale update)
        {
            var locale = await this.Get(id).ConfigureAwait(false);
            if (locale == null)
            {
                throw new NotFoundException<Locale>($"{nameof(Locale.Id)}={id.ToString()}");
            }

            Mapper.Map(update, locale);
        }
    }
}
