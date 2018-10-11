using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class Warehouse
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public DataStatus Status { get; set; }
    }
}
