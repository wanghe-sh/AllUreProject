using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Admin.Models
{
    public class ProductStorageOutput
    {
        public ProductStorageOutput(Product product)
        {
            this.Id = product.Id;
            this.Number = product.Number;
            this.Name = product.Name;
            this.Balance = product.Storage.Balance;
            this.Frozen = product.Storage.Frozen;
            this.TotalStorage = this.Balance - this.Frozen;
            this.BrandName = product.Brand.Name;
            this.CategoryName = product.SubCategory.Parent.Localized.Single(l => l.Language.IsDefault).Name;
            this.SubCategoryName = product.SubCategory.Localized.Single(l => l.Language.IsDefault).Name;
            this.Start = product.Start;
            this.End = product.End;
        }

        public int Id { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public decimal Frozen { get; set; }

        public decimal TotalStorage { get; set; }

        public string BrandName { get; set; }

        public string CategoryName { get; set; }

        public string SubCategoryName { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }
    }
}