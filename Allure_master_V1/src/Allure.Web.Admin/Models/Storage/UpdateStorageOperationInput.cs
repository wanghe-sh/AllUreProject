using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Admin.Models
{
    public class UpdateStorageOperationInput
    {
        public int Id { get; set; }

        public decimal LogisticFee { get; set; }
    }
}
