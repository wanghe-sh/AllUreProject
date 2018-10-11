using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Admin.Models
{
    public class CreateStorageOperationInput
    {
        public StorageOperationType Type { get; set; }

        public string CustomizedCode { get; set; }

        public int? OrderId { get; set; }

        public string WarehouseCode { get; set; }

        public string LogisticCode { get; set; }

        public string LogisticOrderNumber { get; set; }

        public decimal LogisticFee { get; set; }

        public string Comment { get; set; }

        public StorageOperationDetailInput[] Details { get; set; }
    }
}
