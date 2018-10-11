using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allure.Core.Models
{
    public class Language
    {
        public string Code { get; set; }

        public string Description
        {
            get
            {
                return CultureInfo.GetCultureInfo(this.Code).NativeName;
            }
        }

        public bool IsDefault { get; set; }

        public bool Enabled { get; set; }
    }
}
