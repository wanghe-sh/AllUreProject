using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class StorageOperationOutput
    {
        public StorageOperationOutput(StorageOperation operation)
        {
            this.Id = operation.Id;
            this.Type = operation.Type;
            this.OrderId = operation.OrderId;
            this.WarehouseCode = operation.WarehouseCode;
            this.LogisticCode = operation.LogisticCode;
            this.LogisticOrderNumber = operation.LogisticOrderNumber;
            this.LogisticFee = operation.LogisticFee;
            this.Comment = operation.Comment;
            this.Details = operation.Details.Select(d => new StorageOperationDetailOutput(d)).ToArray();
        }

        public int Id { get; set; }

        public StorageOperationType Type { get; set; }

        public string CustomizedCode { get; set; }

        public int? OrderId { get; set; }

        public string WarehouseCode { get; set; }

        public string LogisticCode { get; set; }

        public string LogisticOrderNumber { get; set; }

        public decimal LogisticFee { get; set; }

        public string Comment { get; set; }

        public StorageOperationDetailOutput[] Details { get; set; }
    }
}