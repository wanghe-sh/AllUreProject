using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class StorageOperationListOutput
    {
        public StorageOperationListOutput(StorageOperation operation)
        {
            this.Id = operation.Id;
            this.Type = operation.Type;
            this.OrderId = operation.OrderId;
            this.WarehouseCode = operation.WarehouseCode;
            this.LogisticCode = operation.LogisticCode;
            this.LogisticOrderNumber = operation.LogisticOrderNumber;
            this.LogisticFee = operation.LogisticFee;
            this.CreateBy = operation.CreateBy.ToString();
            this.CreateDate = operation.CreateDate;
            this.UpdateBy = operation.UpdateBy.ToString();
            this.UpdateDate = operation.UpdateDate;
        }

        public int Id { get; set; }

        public StorageOperationType Type { get; set; }

        public string WarehouseCode { get; set; }

        public string LogisticCode { get; set; }

        public string LogisticOrderNumber { get; set; }

        public decimal LogisticFee { get; set; }

        public int? OrderId { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateDate { get; set; }

        public string UpdateBy { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}