using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.Core.Models;

namespace Allure.Core.Criteria.StorageOperations
{
    public class SearchStorageOperation
    {
        public int? StorageOperationId { get; set; }

        public int? OrderId { get; set; }

        public string OrderNo { get; set; }

        public DateTime? MinCreateTime { get; set; }

        public DateTime? MaxCreateTime { get; set; }

        public DateTime? MinUpdateTime { get; set; }

        public DateTime? MaxUpdateTime { get; set; }

        public string LogisticName { get; set; }

        public string LogisticNumber { get; set; }

        public string CreateBy { get; set; }

        public string UpdateBy { get; set; }

        public StorageOperationType? Type { get; set; }

        public string WarehouseName { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}
