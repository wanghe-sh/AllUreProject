using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class SearchStorageOperationInput
    {
        public int? Id { get; set; }

        public int? OrderId { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string LogisticCode { get; set; }

        public string LogisticOrderNumber { get; set; }

        public string CreateBy { get; set; }

        public string UpdateBy { get; set; }

        public StorageOperationType? Type { get; set; }

        public string WarehouseCode { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}