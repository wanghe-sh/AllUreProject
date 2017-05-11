using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class ProductStorage
    {
        public int ProductId { get; set; }

        public decimal Current { get; set; }

        public decimal Frozen { get; set; }

        public DateTime RowVersion { get; set; }
    }
}
