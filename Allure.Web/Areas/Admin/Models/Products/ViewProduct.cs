using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Allure.Web.Areas.Admin.Models.Brands;
using Allure.Web.Areas.Admin.Models.Categories;
using Allure.Web.Areas.Admin.Models.Images;
using Allure.Web.Areas.Admin.Models.Locales;

namespace Allure.Web.Areas.Admin.Models.Products
{
    public class ViewProduct
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public int LocaleId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 货号
        /// </summary>
        public string Number { get; set; }

        public decimal Price { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public TimeSpan? DeliveryTerm { get; set; }

        public string VideoUrl { get; set; }

        /// <summary>
        /// 是否首页小图推荐
        /// </summary>
        public bool RecommendedS { get; set; }

        /// <summary>
        /// 是否首页大图推荐
        /// </summary>
        public bool RecommendedL { get; set; }

        /// <summary>
        /// 当查询不制定排序条件或排序条件相同时，会按照该字段排序
        /// </summary>
        public int DisplayOrder { get; set; }

        public DateTime CreateDate { get; set; }
        
        public bool Deleted { get; set; }

        public ViewImage[] Images { get; set; }

        public ViewLocalization[] Localizations { get; set; }
    }
}