using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Criteria.Warehouses
{
    public class AddWarehouse
    {
        [Required]
        [MaxLength(Config.MaxLength.Warehouse.Name)]
        public string Name { get; set; }
    }
}
