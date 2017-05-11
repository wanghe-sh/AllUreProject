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
    public class AddStorageOperation
    {
        [Required]
        public StorageOperationType? Type { get; set; }

        [OrderRequiredDependsOnType]
        public int? OrderId { get; set; }

        [MaxLength(Config.MaxLength.StorageOperation.PurchaseOrderNumber)]
        public string PurchaseOrderNumber { get; set; }

        [Required]
        public int? WarehouseId { get; set; }
        
        public int? LogisticId { get; set; }
                
        public string LogisticNumber { get; set; }

        public decimal? LogisticExpense { get; set; }

        public string Comment { get; set; }

        [Required]
        [MinLength(1)]
        [ValidateChildren]
        [OrderRequiredDependsOnType]
        public AddStorageOperationDetail[] Details { get; set; }
    }

    public class AddStorageOperationDetail
    {
        public int? OrderDetailId { get; set; }

        [Required]
        public int? ProductId { get; set; }
        
        [Required]
        public decimal? OriginalCount { get; set; }

        [Required]
        public decimal? OperationCount { get; set; }
    }
}
