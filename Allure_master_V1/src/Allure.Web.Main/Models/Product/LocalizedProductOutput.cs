using Allure.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allure.UI.Models
{
    public class LocalizedProductOutput
    {
        public LocalizedProductOutput(LocalizedProduct localizedProduct)
        {
            this.ProductId = localizedProduct.ProductId;
            this.LanguageCode = localizedProduct.LanguageCode;
            this.Descrption = localizedProduct.Description;
        }

        public int ProductId { get; set; }

        public string LanguageCode { get; set; }

        public string Descrption { get; set; }
    }
}