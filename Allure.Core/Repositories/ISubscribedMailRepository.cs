using Allure.Core.Criteria.SubscribedMails;
using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Repositories
{
    public interface ISubscribedMailRepository
    {
        SubscribedMail Add(AddSubscribedMail add);
    }
}
