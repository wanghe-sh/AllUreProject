using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Models;

namespace Allure.Core.Criteria
{
    public class SearchResult<T>
    {
        public int Total { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
