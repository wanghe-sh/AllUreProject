using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Common.Unity;
using Allure.Core.Criteria.Languages;
using Allure.Core.Exceptions;
using Allure.Core.Models;
using Allure.Core.Repositories;
using Allure.Common.Validations;
using AutoMapper;

namespace Allure.Data.Repositories
{
    [Autowire(typeof(ILanguageRepository), Lifetime.PerResolve)]
    class LanguageRepository : ILanguageRepository
    {
        private readonly IDbContext _dbContext;

        public LanguageRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Language> Get(string code)
        {
            return await _dbContext
                .Set<Language>()
                .SingleOrDefaultAsync(l => l.Code == code)
                .ConfigureAwait(false);
        }

        public async Task<Language> GetDefault()
        {
            return await _dbContext
                .Set<Language>()
                .SingleOrDefaultAsync(l => l.IsDefault && l.Enabled)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Language>> GetAll()
        {
            return await _dbContext
                .Set<Language>()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Language>> GetPotential()
        {
            var results = await this.GetAll().ConfigureAwait(false);
            var enabled = results.ToDictionary(l => l.Code);

            return CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Where(c => !enabled.ContainsKey(c.Name))
                .Select(c => new Language { Code = c.Name, Description = c.NativeName });
        }

        public async Task<Language> Add(AddLanguage add)
        {
            var potential = await this.GetPotential().ConfigureAwait(false);

            if (!potential.Any(l => l.Code.Equals(add.Code)))
            {
                throw new ForbiddenException($"{add.Code} is not supported, or already added");
            }

            var language = Mapper.Map<Language>(add);

            if (language.IsDefault)
            {
                var originDefault = await this.GetDefault().ConfigureAwait(false);
                originDefault.IsDefault = false;
            }

            _dbContext.Set<Language>().Add(language);
            return language;
        }

        public async Task Update(string code, UpdateLanguage update)
        {
            var language = await this.Get(code).ConfigureAwait(false);

            if (language == null)
            {
                throw new NotFoundException<Language>($"{nameof(Language.Code)}={code}");
            }

            if (language.IsDefault && (!update.IsDefault || !update.Enabled))
            {
                throw new ForbiddenException("the application needs a default language");
            }

            if (update.IsDefault && update.Enabled)
            {
                var oldDefault = await this.GetDefault().ConfigureAwait(false);
                oldDefault.IsDefault = false;
            }

            Mapper.Map(update, language);
        }
    }
}
