using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Admin.Models
{
    public class StorageOperationDetailOutput
    {
        public StorageOperationDetailOutput(StorageOperationDetail detail)
        {
            this.Id = detail.Id;
            this.ProductId = detail.ProductId;
            this.ProductNumber = detail.Product.Number;
            this.ProductName = detail.Product.Name;
            this.BrandName = detail.Product.Brand.Name;
            this.CategoryName = detail.Product.SubCategory.Parent.Localized.Single(l => l.Language.IsDefault).Name;
            this.SubCategoryName = detail.Product.SubCategory.Localized.Single(l => l.Language.IsDefault).Name;
            this.CurrentBalance = detail.CurrentBalance;
            this.OperationCount = detail.OperationCount;
            this.BalanceAfter = this.CurrentBalance - this.OperationCount;
        }

        public int Id { get; set; }
        
        public int ProductId { get; set; }

        public string ProductNumber { get; set; }

        public string ProductName { get; set; }

        public string BrandName { get; set; }

        public string CategoryName { get; set; }

        public string SubCategoryName { get; set; }

        public decimal CurrentBalance { get; set; }

        public decimal OperationCount { get; set; }

        public decimal BalanceAfter { get; set; }
    }
}
