using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Allure.Common.AutoMapper;
using Allure.Core.Models;

namespace Allure.Web.Areas.Admin.Models.StorageOperations
{
    public class ViewSearchStorageOperation
    {
        public int Id { get; set; }

        public StorageOperationType Type { get; set; }

        public int? OrderId { get; set; }

        public string WarehouseName { get; set; }

        public string LogisticName { get; set; }

        public string LogisticNumber { get; set; }

        public decimal LogisticExpense { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}