using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.Web.Models
{
    public class LocalizedProductOutput
    {
        public LocalizedProductOutput(ProductLocalization localizedProduct)
        {
            this.ProductId = localizedProduct.ProductId;
            this.LanguageCode = localizedProduct.LanguageCode;
            this.Descrption = localizedProduct.Description;
            this.Comment = localizedProduct.Comment;
            this.DeliveryTerm = localizedProduct.DeliveryTerm;
        }

        public int ProductId { get; set; }

        public string LanguageCode { get; set; }

        public string Descrption { get; set; }

        public string Comment { get; set; }

        public string DeliveryTerm { get; set; }
    }
}