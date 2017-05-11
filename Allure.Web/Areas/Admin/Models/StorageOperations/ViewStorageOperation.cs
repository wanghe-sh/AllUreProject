using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;

namespace Allure.Web.Areas.Admin.Models.StorageOperations
{
    public class ViewStorageOperation
    {
        public int Id { get; set; }

        public StorageOperationType Type { get; set; }

        public int? OrderId { get; set; }

        public string OrderNO { get; set; }

        public int WarehouseId { get; set; }

        public int? LogisticId { get; set; }

        public string LogisticNumber { get; set; }

        public decimal LogisticExpense { get; set; }

        public string Comment { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; }

        public ViewStorageOperationDetail[] Details { get; set; }
    }
    
    public class ViewStorageOperationDetail
    {
        public int StorageOperationId { get; set; }

        public int ProductId { get; set; }

        public string ProductNumber { get; set; }

        public string ProductName { get; set; }

        public decimal OriginalCount { get; set; }

        public decimal OperationCount { get; set; }

        public string BrandName { get; set; }

        public string CategoryName { get; set; }
    }
}