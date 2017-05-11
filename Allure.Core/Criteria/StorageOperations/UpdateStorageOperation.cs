using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Models;
using Allure.Common.Validations;
using Allure.Core.Validations.StorageOperations;

namespace Allure.Core.Criteria.StorageOperations
{
    public class UpdateStorageOperation
    {
        [Required]
        public decimal? LogisticExpense { get; set; }
    }
}
