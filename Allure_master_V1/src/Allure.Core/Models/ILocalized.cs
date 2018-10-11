using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public interface ILocalized
    {
        string LanguageCode { get; set; }

        Language Language { get; set; }
    }
}
