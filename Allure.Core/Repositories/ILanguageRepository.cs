using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Criteria.Languages;
using Allure.Core.Models;

namespace Allure.Core.Repositories
{
    public interface ILanguageRepository
    {
        Task<Language> Get(string code);

        Task<Language> GetDefault();

        Task<IEnumerable<Language>> GetAll();

        Task<IEnumerable<Language>> GetPotential();

        Task<Language> Add(AddLanguage add);

        Task Update(string code, UpdateLanguage update);
    }
}